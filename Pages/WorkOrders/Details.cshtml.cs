using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CanopyViewer.Data;
using CanopyViewer.Models;

namespace CanopyViewer.Pages.WorkOrders
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _db;
        public DetailsModel(AppDbContext db) => _db = db;

        public WorkOrder WorkOrder { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var workOrder = await _db.WorkOrders
                .Include(w => w.Asset)
                .Include(w => w.AssignedTo)
                .Include(w => w.AssignedBy)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workOrder == null) return NotFound();

            WorkOrder = workOrder;
            return Page();
        }
    }
}
