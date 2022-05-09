using Mapster;
using MateMachine.CodeChallenge.SocialApp.Data;
using MateMachine.CodeChallenge.SocialApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MateMachine.CodeChallenge.SocialApp.Services;

public class UserService : IUserService {
	private readonly DataContext _context;

	public UserService(DataContext context) {
		ArgumentNullException.ThrowIfNull(context, nameof(context));
		_context = context;
	}

	public async Task<IList<UserDto>> GetAllAsync(long userId, CancellationToken cancellationToken) {
		// TODO: pagination is required!

		var friends = await _context.Friends.Where(_ => _.UserId == userId).ToListAsync(cancellationToken);
		var users = await _context.Users.Where(_ => _.Id != userId).ToListAsync(cancellationToken);
		var dtos = users.Adapt<IList<UserDto>>();

		return dtos;
	}

	public async Task<UserDto?> GetByIdAsync(long userId, long friendId, CancellationToken cancellationToken) {
		var user = await _context.Users.FirstOrDefaultAsync(_ => _.Id == friendId, cancellationToken);
		if (user is null) {
			return null;
		}

		var friend = await _context.Friends.FirstOrDefaultAsync(_ => _.UserId == userId && _.FriendId == friendId, cancellationToken);

		var dto = user.Adapt<UserDto>();

		var hasRequest = await _context.Friends.FirstOrDefaultAsync(_ => _.UserId == friendId && _.FriendId == userId, cancellationToken);
		if (hasRequest is not null && hasRequest.Status == FriendStatus.Pending) {
			dto.Status = FriendStatus.NeedAction;
		}

		if (friend is null) {
			return dto;
		}
		dto.Status = friend.Status;

		return dto;
	}

	public async Task<IList<UserDto>> GetRequestsAsync(long userId, CancellationToken cancellationToken) {
		var friends = await _context.Friends
			.Where(_ => _.FriendId == userId)
			.Select(_ => _.UserId)
			.ToListAsync(cancellationToken);
		if (!friends.Any()) {
			return new List<UserDto>();
		}

		var users = await _context.Users.Where(_ => friends.Contains(_.Id)).AsNoTracking().ToListAsync(cancellationToken);
		var dtos = users.Adapt<IList<UserDto>?>();
		return dtos!;
	}
}
