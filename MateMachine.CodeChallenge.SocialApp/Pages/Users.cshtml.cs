using MateMachine.CodeChallenge.SocialApp.Entities;
using MateMachine.CodeChallenge.SocialApp.Models;
using MateMachine.CodeChallenge.SocialApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MateMachine.CodeChallenge.SocialApp.Pages;

[Authorize]
public class UsersPage : PageModel {
	private readonly UserManager<AppUser> _userManager;
	private readonly IUserService _userService;

	public UsersPage(
		UserManager<AppUser> userManager,
		IUserService userService) {
		ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
		ArgumentNullException.ThrowIfNull(userService, nameof(userService));

		_userManager = userManager;
		_userService = userService;
	}

	public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken) {
		var user = await _userManager.FindByNameAsync(User.Identity.Name);
		if (user is null) {
			return RedirectToPage("./Account/Login", new {
				returnUrl = Url.Page("./Friends")
			});
		}
		UsersDto = await _userService.GetAllAsync(user.Id, cancellationToken);
		return Page();
	}

	public IList<UserDto>? UsersDto { get; set; }
}
