namespace MateMachine.CodeChallenge.SocialApp.Models;

public class UserDto {
	public long Id { get; set; }

	public string FirstName { get; set; }

	public string LastName { get; set; }

	public string FullName { get; set; }

	public FriendStatus Status { get; set; }
}
