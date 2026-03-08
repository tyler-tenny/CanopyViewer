using CanopyViewer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CanopyViewer.Models;
using Microsoft.EntityFrameworkCore;

namespace CanopyViewer.Pages.Assets
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db) => _db = db;
        public List<Asset> Assets { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string? Sort { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Dir { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Asset> query = _db.Assets;

            if (!string.IsNullOrWhiteSpace(Search))
            {
                var searchLower = Search.ToLower();
                query = query.Where(a =>
                a.Name.ToLower().Contains(searchLower) ||
                (a.Type != null && a.Type.ToLower().Contains(searchLower)));
            }

            bool desc = Dir == "desc";
            query = Sort switch
            {
                "Type" => desc ? query.OrderByDescending(a => a.Type) : query.OrderBy(a => a.Type),
                "Status" => desc ? query.OrderByDescending(a => a.Status) : query.OrderBy(a => a.Status),
                "Id" => desc ? query.OrderByDescending(a => a.Id) : query.OrderBy(a => a.Id),
                 _ => desc ? query.OrderByDescending(a => a.Name) : query.OrderBy(a => a.Name),
            };
            Assets = await query.ToListAsync();
        }

        public string SortLink(string column)
        {
            string dir = (Sort == column && Dir == "asc") ? "desc" : "asc";
            return $"?sort={column}&dir={dir}&search={Search}";
        }

        public string SortIcon(string column)
        {
            if (Sort != column) return "bi bi-arrow-down-up text-muted";
            return Dir == "asc" ? "bi bi-arrow-up" : "bi bi-arrow-down";
        }
    }
}
