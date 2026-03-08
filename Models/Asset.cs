using System.ComponentModel.DataAnnotations;

namespace CanopyViewer.Models
{
    public class Asset
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [StringLength(20)]
        public string Status { get; set; } = "Active";
        [StringLength(255)]
        public string? Location { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
            = new List<WorkOrder>();
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
