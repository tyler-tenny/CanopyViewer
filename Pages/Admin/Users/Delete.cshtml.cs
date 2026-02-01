using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CanopyViewer.Data;
using CanopyViewer.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CanopyViewer.Pages.Admin.Users
{
    [Authorize(Roles ="Admin")]
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _db;
        public DeleteModel(AppDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public User UserToDelete { get; set; } = default;
        public IActionResult OnGet(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null) return NotFound();

            UserToDelete = user;

            if (user.Username == User.Identity!.Name) ModelState.AddModelError("", "You cannot delete your own account!");

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null) return NotFound();

            if (user.Username == User.Identity!.Name)
            {
                ModelState.AddModelError("", "You cannot delete your own account!");
                UserToDelete = user;
                return Page();
            }

            _db.Users.Remove(user);
            _db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
