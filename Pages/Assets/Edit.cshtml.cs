using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CanopyViewer.Data;
using CanopyViewer.Models;

namespace CanopyViewer.Pages.Assets
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        public EditModel(AppDbContext db) => _db = db;
    
        [BindProperty]
        public Asset Input { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var asset = await _db.Assets.FindAsync(id);
            if (asset == null) return NotFound();
    
            Input = asset;
            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _db.Attach(Input).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToPage("Details", new { id = Input.Id });
        }
    }
}
