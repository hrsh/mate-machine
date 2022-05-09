using Microsoft.AspNetCore.Identity;

namespace MateMachine.CodeChallenge.SocialApp.Models;

public sealed class AppUser : IdentityUser<long> {
	public string FirstName { get; private set; }

	public string LastName { get; private set; }

	public string FullName => $"{FirstName} {LastName}";

	public IList<AppUserToken> UserTokens { get; private set; }

	public IList<AppUserRole> Roles { get; private set; }

	public IList<AppUserLogin> Logins { get; private set; }

	public IList<AppUserClaim> Claims { get; private set; }

	public static AppUser New(
		long userId,
		string firstName,
		string lastName,
		string email,
		string phoneNumber,
		string username) {
		var user = new AppUser {
			FirstName = firstName,
			LastName = lastName,
			Email = email,
			PhoneNumber = phoneNumber,
			Id = userId,
			UserName = username
		};

		return user;
	}
}
