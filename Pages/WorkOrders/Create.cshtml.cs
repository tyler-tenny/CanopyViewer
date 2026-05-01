using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CanopyViewer.Models;
using CanopyViewer.Data;
using Microsoft.EntityFrameworkCore;

namespace CanopyViewer.Pages.WorkOrders
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        public CreateModel(AppDbContext db) => _db = db;
        [BindProperty]
        public WorkOrder Input { get; set; } = new();
        public List<Asset> Assets { get; set; } = new();
        public List<User> Users { get; set; }
        public async Task OnGetAsync(int? assetId = null)
        {
            Assets = await _db.Assets.OrderBy(a => a.Name).ToListAsync();
            Users = await _db.Users.OrderBy(u => u.Username).ToListAsync();

            Input.CreatedBy = User.Identity?.Name ?? string.Empty;
            Input.Status = "New";
            Input.RecurrenceType = "One-Time";

            var currentUser = await _db.Users
                .FirstOrDefaultAsync(u => u.Username == User.Identity!.Name);
            if (currentUser != null)
                Input.AssignedById = currentUser.Id;
            if (assetId.HasValue)
                Input.AssetId = assetId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Assets = await _db.Assets.OrderBy(a => a.Name).ToListAsync();
                Users = await _db.Users.OrderBy(u => u.Username).ToListAsync();
                return Page();
            }

            Input.CreatedDate = DateTime.UtcNow;

            if (Input.RecurrenceType == "Recurring" && Input.StartDate.HasValue)
                Input.NextOccurrence = Input.StartDate;

            //Set UTC for Npgsql
            if (Input.StartDate.HasValue)
                Input.StartDate = DateTime.SpecifyKind(Input.StartDate.Value, DateTimeKind.Utc);

            if (Input.NextOccurrence.HasValue)
                Input.NextOccurrence = DateTime.SpecifyKind(Input.NextOccurrence.Value, DateTimeKind.Utc);

            _db.WorkOrders.Add(Input);
            await _db.SaveChangesAsync();

            return RedirectToPage("Details", new { id = Input.Id });
        }
    }
}

