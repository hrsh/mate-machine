using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MateMachine.CodeChallenge.SocialApp.Pages;
public class IndexPage : PageModel {
	public IActionResult OnGet() {
		return Page();
	}
}
