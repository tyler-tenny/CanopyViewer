namespace CanopyViewer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Viewer";

        public string? Email { get; set; }
        public bool NotifyOnNewWorkOrder { get; set; } = false;

        public virtual ICollection<WorkOrder> AssignedWorkOrders { get; set; }
             = new List<WorkOrder>();

        public virtual ICollection<WorkOrder> AssignedByWorkOrders { get; set; }
             = new List<WorkOrder>();
    }

    public static class Role
    {
        public const string Admin = "Admin";
        public const string Viewer = "Viewer";
        public const string User = "User";
    }
}
