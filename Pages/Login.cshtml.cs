using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ContactManager.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            if (Username == "admin" && Password == "123")
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Username)
            };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                var principal = new ClaimsPrincipal(identity);

                var authProps = new AuthenticationProperties
                {
                    IsPersistent = false
                };

                await HttpContext.SignInAsync("MyCookieAuth", principal, authProps);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
            return Page();
        }
    }
}