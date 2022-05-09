namespace MateMachine.CodeChallenge.SocialApp.Models;

public sealed class FriendRequest {
	private FriendRequest() { }

	public long Id { get; private set; }

	public long FromId { get; private set; }

	public long ToId { get; private set; }

	public bool Accepted { get; private set; }

	public DateTimeOffset RequestedAt { get; private set; }

	public DateTimeOffset? ReactedAt { get; private set; }
}
