using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CanopyViewer.Data;
using CanopyViewer.Models;
using System.Security.Claims;
using CanopyViewer.Services;

namespace CanopyViewer.Pages.Admin.Users
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        public EditModel(AppDbContext db)
        {
            _db = db;   
        }
        [BindProperty]
        public EditUserModel Input { get; set; } = new();
        public IActionResult OnGet(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null) return NotFound();
            
            Input.Username = user.Username;
            Input.Role = user.Role;
            Input.Email = user.Email;
            Input.NotifyOnNewWorkOrder = user.NotifyOnNewWorkOrder;

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = _db.Users.Find(id);
            if (user == null) return NotFound();

            if (user.Id ==  currentUserId && Input.Role != "Admin")
            {
                ModelState.AddModelError("", "You cannot remove your own admin role!");
                return Page();
            }

            if (_db.Users.Any(u => u.Username == Input.Username && u.Id != id))
            {
                ModelState.AddModelError("", "Username already exists!");
                return Page();
            }

            if (!string.IsNullOrEmpty(Input.Password))
            {
                if (Input.Password.Length >= 6)
                {
                    user.PasswordHash = PasswordService.Hash(Input.Password);
                }
                else
                {
                    ModelState.AddModelError("", "Password must be at least 6 characters.");
                    return Page();
                }
            }

            user.Username = Input.Username;
            user.Email = Input.Email;
            user.NotifyOnNewWorkOrder = Input.NotifyOnNewWorkOrder;
            if (Input.Role != user.Role && Input.Role != string.Empty && ModelState.IsValid) user.Role = Input.Role;

            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
