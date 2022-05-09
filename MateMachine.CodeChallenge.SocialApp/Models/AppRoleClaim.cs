using Microsoft.AspNetCore.Identity;

namespace MateMachine.CodeChallenge.SocialApp.Models;

public sealed class AppRoleClaim : IdentityRoleClaim<long> {
	public AppRole Role { get; private set; }
}
