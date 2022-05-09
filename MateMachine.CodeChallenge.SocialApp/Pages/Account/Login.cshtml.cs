using MateMachine.CodeChallenge.SocialApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MateMachine.CodeChallenge.SocialApp.Pages.Account;

[AllowAnonymous]
public class LoginPage : PageModel {
	private readonly UserManager<AppUser> _userManager;
	private readonly SignInManager<AppUser> _signInManager;
	private readonly ILogger _logger;

	public LoginPage(
		UserManager<AppUser> userManager,
		SignInManager<AppUser> signInManager,
		ILogger<LoginPage> logger) {
		ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
		ArgumentNullException.ThrowIfNull(signInManager, nameof(signInManager));
		ArgumentNullException.ThrowIfNull(logger, nameof(logger));

		_userManager = userManager;
		_signInManager = signInManager;
		_logger = logger;
	}

	[BindProperty]
	public string ReturnUrl { get; set; }

	public IActionResult OnGetAsync(string returnUrl) {
		if (_signInManager.IsSignedIn(User)) {
			return RedirectToPage("/Index");
		}

		returnUrl ??= Url.Content("~/");

		ReturnUrl = returnUrl;
		return Page();
	}

	public async Task<IActionResult> OnPostAsync(string returnUrl) {
		returnUrl ??= Url.Content("~/");

		if (!ModelState.IsValid) {
			return RedirectToPage();
		}

		var loginStatus = await _signInManager.PasswordSignInAsync(
			LoginViewModel.Username,
			LoginViewModel.Password,
			LoginViewModel.Remember,
			true);

		if (loginStatus.Succeeded) {
			return Redirect(returnUrl);
		}

		return Page();
	}

	[BindProperty]
	public LoginModel LoginViewModel { get; set; }

	public class LoginModel {
		[Required]
		[StringLength(64, MinimumLength = 3)]
		public string Username { get; set; }

		[Required]
		[StringLength(64, MinimumLength = 3)]
		public string Password { get; set; }

		public bool Remember { get; set; }
	}
}
