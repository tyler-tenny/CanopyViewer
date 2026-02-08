using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CanopyViewer.Pages.WorkOrders
{
    public class IndexModel : PageModel
    {
        public List<WorkOrderRow> WorkOrders { get; set; } = new();
        public void OnGet()
        {
            WorkOrders = new()
            {
                new WorkOrderRow
                {
                    Id = 1,
                    Name = "Example Asset",
                    Type = "HVAC",
                    DueDate = "4/23/26"
                }
            };
        }
    }

    public class WorkOrderRow
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string DueDate { get; set; } = string.Empty;
    }
}
