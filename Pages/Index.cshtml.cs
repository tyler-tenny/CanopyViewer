using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CanopyViewer.Data;
using CanopyViewer.Models;

namespace CanopyViewer.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db) => _db = db;
    public List<WorkOrder> AssignedToMe { get; set; } = new();
    public List<WorkOrder> CreatedByMe { get; set; } = new();
    public int TotalAssets { get; set; }
    public int OpenWorkOrders { get; set; }
    public int OverdueWorkOrders { get; set; }

    public async Task OnGetAsync()
    {
        var currentUserId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        AssignedToMe = await _db.WorkOrders
            .Include(w => w.Asset)
            .Where(w => w.AssignedToId == currentUserId
                && w.Status != "Completed")
            .OrderBy(w => w.NextOccurrence ?? w.CreatedDate)
            .ToListAsync();

        CreatedByMe = await _db.WorkOrders
            .Include(w => w.Asset)
            .Where(w => w.CreatedBy == User.Identity!.Name)
            .OrderByDescending(w => w.CreatedDate)
            .Take(10)
            .ToListAsync();

        TotalAssets = await _db.Assets.CountAsync();
        OpenWorkOrders = await _db.WorkOrders
            .CountAsync(w => w.Status != "Completed");
    }
}
