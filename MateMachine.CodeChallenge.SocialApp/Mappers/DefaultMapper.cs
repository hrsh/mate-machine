using Mapster;
using MateMachine.CodeChallenge.SocialApp.Entities;
using MateMachine.CodeChallenge.SocialApp.Models;

namespace MateMachine.CodeChallenge.SocialApp.Mappers;
public class DefaultMapper {
	public DefaultMapper() {
		TypeAdapterConfig<AppUser, FriendDto>.NewConfig();
		TypeAdapterConfig<AppUser, UserDto>.NewConfig()
			.Map(_ => _.FullName, _ => _.FullName);
	}
}
