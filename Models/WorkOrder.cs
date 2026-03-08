using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string ActionTaken { get; set; } = string.Empty;

        [StringLength(20)]
        public string Status { get; set; } = "New";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;

        public int? AssignedById { get; set; }
        [ForeignKey("AssignedById")]
        public virtual User? AssignedBy { get; set; }
        public int? AssignedToId { get; set; }
        [ForeignKey("AssignedToId")]
        public virtual User? AssignedTo { get; set; }

        [StringLength(20)]
        public string RecurrenceType { get; set; } = "One-Time";

        [StringLength(20)]
        public string? RecurrenceInterval { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? NextOccurrence { get; set; }
        public int? RecurCount { get; set; }

        public int? AssetId { get; set; }
        [ForeignKey("AssetId")]
        public virtual Asset? Asset { get; set; }
    }
}
