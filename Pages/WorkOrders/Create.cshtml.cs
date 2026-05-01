using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CanopyViewer.Models;
using CanopyViewer.Data;
using Microsoft.EntityFrameworkCore;
using CanopyViewer.Services;

namespace CanopyViewer.Pages.WorkOrders
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly EmailService _emailService;
        public CreateModel(AppDbContext db, EmailService emailService)
        {
            _db = db;
            _emailService = emailService;
        }

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

            if (Input.RecurrenceType == "Recurring" && Input.StartDate.HasValue)
                Input.NextOccurrence = Input.StartDate;

            //Set UTC for Npgsql
            if (Input.StartDate.HasValue)
                Input.StartDate = DateTime.SpecifyKind(Input.StartDate.Value, DateTimeKind.Utc);

            if (Input.NextOccurrence.HasValue)
                Input.NextOccurrence = DateTime.SpecifyKind(Input.NextOccurrence.Value, DateTimeKind.Utc);

            Input.CreatedDate = Input.StartDate.HasValue ? DateTime.SpecifyKind(Input.StartDate.Value, DateTimeKind.Utc) : DateTime.UtcNow;

            _db.WorkOrders.Add(Input);
            await _db.SaveChangesAsync();
            
            //Find users with email enabled and valid emails
            var notifyUsers = await _db.Users
                .Where(u => u.NotifyOnNewWorkOrder && u.Email != null)
                .ToListAsync();

            //send notifications
            foreach (var user in notifyUsers)
            {
                await _emailService.SendWorkOrderNotificationAsync(
                    user.Email!,
                    user.Username,
                    Input.Title,
                    Input.Id);
            }

            return RedirectToPage("Details", new { id = Input.Id });
        }
    }
}

