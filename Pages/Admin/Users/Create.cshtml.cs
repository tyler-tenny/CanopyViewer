using CanopyViewer.Data;
using CanopyViewer.Models;
using CanopyViewer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CanopyViewer.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;

        [BindProperty]
        public CreateUserInput Input { get; set; } = new();
        public CreateModel(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            if (_db.Users.Any(u => u.Username == Input.Username))
            {
                ModelState.AddModelError("", "Username already exists");
                return Page();
            }

            var user = new User
            {
                Username = Input.Username,
                PasswordHash = PasswordService.Hash(Input.Password),
                Role = Input.Role
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
