using MateMachine.CodeChallenge.SocialApp.Entities;
using MateMachine.CodeChallenge.SocialApp.Models;
using MateMachine.CodeChallenge.SocialApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MateMachine.CodeChallenge.SocialApp.Pages;

[Authorize]
public class FriendRequestPage : PageModel {
	private readonly UserManager<AppUser> _userManager;
	private readonly IUserService _userService;
	private readonly IFriendService _friendService;

	public FriendRequestPage(
		UserManager<AppUser> userManager,
		IUserService userService,
		IFriendService friendService) {
		ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
		ArgumentNullException.ThrowIfNull(userService, nameof(userService));
		ArgumentNullException.ThrowIfNull(friendService, nameof(friendService));

		_userManager = userManager;
		_userService = userService;
		_friendService = friendService;
	}

	public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken) {
		var user = await _userManager.FindByNameAsync(User.Identity.Name);
		if (user is null) {
			return RedirectToPage("./Account/Login", new {
				returnUrl = Url.Page("./Friends")
			});
		}
		Requests = await _userService.GetRequestsAsync(user.Id, cancellationToken);
		return Page();
	}

	public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken) {
		var user = await _userManager.FindByNameAsync(User.Identity.Name);
		if (user is null) {
			return RedirectToPage("./Account/Login", new {
				returnUrl = Url.Page("./Friends")
			});
		}

		await _friendService.AcceptAsync(user.Id, FriendId, cancellationToken);
		return Page();
	}

	public IList<UserDto> Requests { get; set; }

	[BindProperty]
	public long FriendId { get; set; }
}
