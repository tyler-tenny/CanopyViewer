using CanopyViewer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace CanopyViewer.Pages
{
    [Authorize(Roles = Role.Admin)]
    public class AdminPageModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
