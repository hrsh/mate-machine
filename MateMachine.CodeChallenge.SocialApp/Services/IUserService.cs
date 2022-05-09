using MateMachine.CodeChallenge.SocialApp.Models;

namespace MateMachine.CodeChallenge.SocialApp.Services;

public interface IUserService {
	Task<IList<UserDto>> GetAllAsync(long userId, CancellationToken cancellationToken);

	Task<UserDto?> GetByIdAsync(long userId, long friendId, CancellationToken cancellationToken);

	Task<IList<UserDto>> GetRequestsAsync(long userId, CancellationToken cancellationToken);
}
