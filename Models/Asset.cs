using System.ComponentModel.DataAnnotations;

namespace CanopyViewer.Models
{
    public class Asset
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [StringLength(20)]
        public string Status { get; set; } = "Active";
        [StringLength(255)]
        public string? Location { get; set; }
    }
}
