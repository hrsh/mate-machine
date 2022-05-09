using Microsoft.AspNetCore.Identity;

namespace MateMachine.CodeChallenge.SocialApp.Entities;

public sealed class AppUserClaim : IdentityUserClaim<long> {
	public AppUser User { get; private set; }
}
