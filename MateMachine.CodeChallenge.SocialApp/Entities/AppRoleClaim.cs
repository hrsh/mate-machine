using Microsoft.AspNetCore.Identity;

namespace MateMachine.CodeChallenge.SocialApp.Entities;

public sealed class AppRoleClaim : IdentityRoleClaim<long> {
	public AppRole Role { get; private set; }
}
