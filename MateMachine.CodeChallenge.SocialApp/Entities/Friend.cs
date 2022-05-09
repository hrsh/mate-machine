using MateMachine.CodeChallenge.SocialApp.Models;

namespace MateMachine.CodeChallenge.SocialApp.Entities;

public sealed class Friend {
	private Friend() {

	}

	public long Id { get; private set; }

	public long UserId { get; private set; }

	public long FriendId { get; private set; }

	public FriendStatus Status { get; private set; }

	public static Friend New(
		long id,
		long userId,
		long friendId) {
		var entity = new Friend {
			Id = id,
			UserId = userId,
			FriendId = friendId,
			Status = FriendStatus.Pending
		};
		return entity;
	}

	public void Accept() {
		Status = FriendStatus.Friend;
	}
}
