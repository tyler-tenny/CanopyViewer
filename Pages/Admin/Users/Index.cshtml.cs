using CanopyViewer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CanopyViewer.Models;
namespace CanopyViewer.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public List<User> Users { get; set; } = new();
        public IndexModel(AppDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Users = _db.Users.OrderBy(u => u.Username).ToList();
        }
    }
}
