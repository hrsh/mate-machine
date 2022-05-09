using Microsoft.AspNetCore.Identity;

namespace MateMachine.CodeChallenge.SocialApp.Entities;

public sealed class AppUserRole : IdentityUserRole<long> {
	public AppUser User { get; private set; }

	public AppRole Role { get; private set; }
}
