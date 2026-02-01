using System.ComponentModel.DataAnnotations;

namespace CanopyViewer.Models
{
    public class CreateUserInput
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role {get; set; } = string.Empty;
    }
}
