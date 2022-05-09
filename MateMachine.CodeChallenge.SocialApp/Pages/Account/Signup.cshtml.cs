using IdGen;
using MateMachine.CodeChallenge.SocialApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MateMachine.CodeChallenge.SocialApp.Pages.Account;
public class SignupPage : PageModel {
	private readonly UserManager<AppUser> _userManager;

	public SignupPage(
		UserManager<AppUser> userManager) {
		_userManager = userManager;
	}

	public IActionResult OnGet(string returnUrl) {
		returnUrl ??= Url.Content("~/");

		ReturnUrl = returnUrl;
		return Page();
	}

	public async Task<IActionResult> OnPostAsync() {
		if (!ModelState.IsValid) {
			return RedirectToPage();
		}
		var user = AppUser.New(
			new IdGenerator(0).CreateId(),
			SignupViewModel.FirstName,
			SignupViewModel.LastName,
			SignupViewModel.Email,
			SignupViewModel.PhoneNumber,
			SignupViewModel.UserName);

		var identityResult = await _userManager.CreateAsync(user, SignupViewModel.Password);
		if (identityResult.Succeeded) {
			return RedirectToPage("./Login", new { returnUrl = ReturnUrl });
		}

		return Page();
	}

	[BindProperty]
	public string ReturnUrl { get; set; }

	[BindProperty]
	public SignupModel SignupViewModel { get; set; }

	public class SignupModel {
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public string Password { get; set; }
	}
}
