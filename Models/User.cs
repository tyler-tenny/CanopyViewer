namespace CanopyViewer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Viewer";
    }

    public static class Role
    {
        public const string Admin = "Admin";
        public const string Viewer = "Viewer";
        public const string User = "User";
    }
}
