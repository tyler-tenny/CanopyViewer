using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CanopyViewer.Pages.Assets
{
    public class IndexModel : PageModel
    {
        public List<AssetRow> Assets { get; set; } = new();
        public void OnGet()
        {
            Assets = new()
            {
                new AssetRow
                {
                    Id = 1,
                    Name = "Example Asset",
                    Type = "HVAC",
                    Status = "Active"
                }
            };
        }
    }

    public class AssetRow
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
