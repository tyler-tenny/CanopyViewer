using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CanopyViewer.Data;
using CanopyViewer.Models;

namespace CanopyViewer.Pages.WorkOrders
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        public EditModel(AppDbContext db) => _db = db;

        [BindProperty]
        public WorkOrder Input { get; set; } = null!;

        public List<Asset> Assets { get; set; } = new();
        public List<User> Users { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var workOrder = await _db.WorkOrders.FindAsync(id);
            if (workOrder == null) return NotFound();

            Input = workOrder;
            Assets = await _db.Assets.OrderBy(a => a.Name).ToListAsync();
            Users = await _db.Users.OrderBy(u => u.Username).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Assets = await _db.Assets.OrderBy(a => a.Name).ToListAsync();
                Users = await _db.Users.OrderBy(u => u.Username).ToListAsync();
                return Page();
            }

            _db.Attach(Input).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToPage("Details", new { id = Input.Id });
        }
    }
}
