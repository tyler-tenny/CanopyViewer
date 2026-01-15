using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using CanopyViewer.Data;
using CanopyViewer.Services;

namespace CanopyViewer.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _db;
        public LoginModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == Username);
            if (user == null || !PasswordService.Verify(Password, user.PasswordHash))
            {
                ErrorMessage = "Invalid username or password";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Cookies", principal);

            return RedirectToPage("/Index");
        }
        public void OnGet()
        {
        }
    }
}
