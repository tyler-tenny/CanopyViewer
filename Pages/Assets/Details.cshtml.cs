using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using CanopyViewer.Models;

namespace CanopyViewer.Pages.Assets
{
    public class DetailsModel : PageModel
    {
        public Asset asset { get; set; } = new();
        public void OnGet(int id)
        { }
    }
}
