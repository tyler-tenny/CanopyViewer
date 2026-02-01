using System.ComponentModel.DataAnnotations;

namespace CanopyViewer.Models
{
    public class EditUserModel
    {
        public string Username { get; set; } = "";

        // ❗ Optional on edit
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        public string Role { get; set; } = "";
    }
}
