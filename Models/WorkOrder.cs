using System.ComponentModel.DataAnnotations;

namespace CanopyViewer.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        [StringLength(500)]
        public string Status { get; set; } = "New";
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public string? AssignedTo { get; set; }
        public int? AssetId { get; set; }
    }
}
