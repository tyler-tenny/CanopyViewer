using CanopyViewer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CanopyViewer.Models;
using Microsoft.EntityFrameworkCore;

namespace CanopyViewer.Pages.WorkOrders
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db) => _db = db;
        public List<WorkOrder> WorkOrders { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string? Search {  get; set; }
        [BindProperty(SupportsGet = true)]
        public string? StatusFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Sort { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Dir { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<WorkOrder> query = _db.WorkOrders
                .Include(w => w.Asset)
                .Include(w => w.AssignedTo)
                .Include(w => w.AssignedBy);

            if(!string.IsNullOrWhiteSpace(Search))
            {
                var s = Search.ToLower();
                query = query.Where(w =>
                    w.Title.ToLower().Contains(s) ||
                    w.Description.ToLower().Contains(s) ||
                    (w.AssignedTo != null && w.AssignedTo.Username.ToLower().Contains(s)));
            }

            if (!string.IsNullOrWhiteSpace(StatusFilter))
                query = query.Where(w => w.Status == StatusFilter);
            bool desc = Dir == "desc";
            query = Sort switch
            {
                "Status" => desc ? query.OrderByDescending(w => w.Status)
                                 : query.OrderBy(w => w.Status),
                "AssignedTo" => desc ? query.OrderByDescending(w => w.AssignedTo)
                                 : query.OrderBy(w => w.AssignedTo),
                "CreatedDate" => desc ? query.OrderByDescending(w => w.CreatedDate)
                                 : query.OrderBy(w => w.CreatedDate),
                "Id" => desc ? query.OrderByDescending(w => w.Id)
                                 : query.OrderBy(w => w.Id),
                _ => desc ? query.OrderByDescending(w => w.Status)
                                 : query.OrderBy(w => w.Status),
            };

            WorkOrders = await query.ToListAsync();
        }

        public string SortLink(string column)
        {
            string dir = (Sort == column && Dir == "asc") ? "desc" : "asc";
            return $"?sort={column}&dir={dir}&search={Search}&statusFilter={StatusFilter}";
        }

        public string SortIcon(string column)
        {
            if (Sort != column) return "bi bi-arrow-down-up text-muted";
            return Dir == "asc" ? "bi bi-arrow-up" : "bi bi-arrow-down";
        }
    }
}
