using MateMachine.CodeChallenge.SocialApp.Entities;
using MateMachine.CodeChallenge.SocialApp.Models;
using MateMachine.CodeChallenge.SocialApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MateMachine.CodeChallenge.SocialApp.Pages;

[Authorize]
public class FriendsPage : PageModel {
	private readonly UserManager<AppUser> _userManager;
	private readonly IFriendService _friendService;

	public FriendsPage(
		UserManager<AppUser> userManager,
		IFriendService friendService) {
		ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
		ArgumentNullException.ThrowIfNull(friendService, nameof(friendService));

		_userManager = userManager;
		_friendService = friendService;
	}

	public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken) {
		var user = await _userManager.FindByNameAsync(User.Identity.Name);
		if (user is null) {
			return RedirectToPage("./Account/Login", new {
				returnUrl = Url.Page("./Friends")
			});
		}
		FriendsList = await _friendService.GetFriendsAsync(user.Id, cancellationToken);
		return Page();
	}

	public IList<FriendDto>? FriendsList { get; set; }
}
