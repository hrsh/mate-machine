using MateMachine.CodeChallenge.SocialApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MateMachine.CodeChallenge.SocialApp.Pages.Account;
public class SignoutPage : PageModel {
	private readonly SignInManager<AppUser> _signInManager;

	public SignoutPage(SignInManager<AppUser> signInManager) {
		_signInManager = signInManager;
	}

	public async Task<IActionResult> OnGetAsync() {
		await _signInManager.SignOutAsync();
		return RedirectToPage("./Index");
	}
}

