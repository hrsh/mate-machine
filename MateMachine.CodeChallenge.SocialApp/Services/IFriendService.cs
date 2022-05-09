using MateMachine.CodeChallenge.SocialApp.Models;

namespace MateMachine.CodeChallenge.SocialApp.Services;

public interface IFriendService {
	Task AcceptAsync(long userId, long friendId, CancellationToken cancellationToken);

	Task RequestAsync(long userId, long friendId, CancellationToken cancellationToken);

	Task<IList<FriendDto>> GetFriendsAsync(long userId, CancellationToken cancellationToken);
}
