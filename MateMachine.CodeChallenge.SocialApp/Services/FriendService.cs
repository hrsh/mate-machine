using IdGen;
using Mapster;
using MateMachine.CodeChallenge.SocialApp.Data;
using MateMachine.CodeChallenge.SocialApp.Entities;
using MateMachine.CodeChallenge.SocialApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MateMachine.CodeChallenge.SocialApp.Services;

public class FriendService : IFriendService {
	private readonly DataContext _context;

	public FriendService(DataContext context) {
		ArgumentNullException.ThrowIfNull(context, nameof(context));
		_context = context;
	}

	public async Task RequestAsync(long userId, long friendId, CancellationToken cancellationToken) {
		if (userId == friendId) {
			return;
		}

		var user = await _context.Users
			.Where(_ => _.Id == userId)
			.Include(_ => _.Friends)
			.FirstOrDefaultAsync(cancellationToken);
		if (user is null) {
			return;
		}

		if (user.Friends.Any(_ => _.Id == friendId)) {
			return;
		}

		var friend = Friend.New(new IdGenerator(0).CreateId(), userId, friendId);
		await _context.Friends.AddAsync(friend, cancellationToken);

		user.Friends.Add(friend);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<IList<FriendDto>> GetFriendsAsync(long userId, CancellationToken cancellationToken) {
		var friends = await _context.Friends.Where(_ => _.UserId == userId)
			.ToListAsync(cancellationToken);
		if (!friends.Any()) {
			return new List<FriendDto>();
		}

		var friendsId = friends.Select(_ => _.FriendId);
		var users = await _context.Users.Where(_ => friendsId.Contains(_.Id)).ToListAsync(cancellationToken);

		var dtos = users.Adapt<List<FriendDto>>();
		foreach (var dto in dtos) {
			var friend = friends.FirstOrDefault(_ => _.FriendId == dto.Id);
			if (friend is null) {
				continue;
			}

			dto.Status = friend.Status;
		}
		return dtos;
	}

	public async Task AcceptAsync(long userId, long friendId, CancellationToken cancellationToken) {
		var request = await _context.Friends.FirstOrDefaultAsync(_ => _.FriendId == userId && _.UserId == friendId, cancellationToken);
		if (request is null || request.Status == FriendStatus.Friend) {
			return;
		}
		var user = await _context.Users
			.Where(_ => _.Id == userId)
			.Include(_ => _.Friends)
			.FirstOrDefaultAsync(cancellationToken);
		if (user is null) {
			return;
		}

		if (user.Friends.Any(_ => _.Id == friendId)) {
			return;
		}

		request.Accept();

		var friend = Friend.New(new IdGenerator(0).CreateId(), userId, friendId);
		friend.Accept();
		await _context.Friends.AddAsync(friend, cancellationToken);

		user.Friends.Add(friend);
		await _context.SaveChangesAsync(cancellationToken);
	}
}
