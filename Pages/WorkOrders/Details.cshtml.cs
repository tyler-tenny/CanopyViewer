using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using CanopyViewer.Models;

namespace CanopyViewer.Pages.WorkOrders
{
    public class DetailsModel : PageModel
    {
        public WorkOrder workOrder { get; set; } = new();
        public void OnGet(int id)
        { }
    }
}
