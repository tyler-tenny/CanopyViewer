using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CanopyViewer.Data;
using CanopyViewer.Models;

namespace CanopyViewer.Pages.Assets
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _db;
        public DetailsModel(AppDbContext db) => _db = db;
        public Asset Asset { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var asset = await _db.Assets
                .Include(a => a.WorkOrders)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asset == null) return NotFound();

            Asset = asset;
            return Page();
        }
    }
}
