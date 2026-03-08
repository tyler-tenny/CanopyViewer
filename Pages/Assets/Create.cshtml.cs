using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CanopyViewer.Models;
using CanopyViewer.Data;

namespace CanopyViewer.Pages.Assets
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        public CreateModel(AppDbContext db) => _db = db;

        [BindProperty]
        public Asset Input { get; set; } = new();

        public void OnGet()
        {
            Input.CreatedBy = User.Identity?.Name ?? string.Empty;
            Input.CreatedDate = DateTime.UtcNow;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _db.Assets.Add(Input);
            await _db.SaveChangesAsync();

            return RedirectToPage("Details", new {id = Input.Id});
        }
    }
}
