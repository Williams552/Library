using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library_Web.Pages.Members
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("fullName");
            return RedirectToPage("/Index");
        }
    }
}
