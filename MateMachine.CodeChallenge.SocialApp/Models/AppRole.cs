using Microsoft.AspNetCore.Identity;

namespace MateMachine.CodeChallenge.SocialApp.Models;

public sealed class AppRole : IdentityRole<long> {
	public IList<AppUserRole> Users { get; private set; }

	public IList<AppRoleClaim> Claims { get; private set; }

	public static AppRole New(long id, string name) {
		var role = new AppRole {
			Id = id,
			Name = name
		};
		return role;
	}
}
