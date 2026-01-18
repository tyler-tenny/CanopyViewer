using BCrypt.Net;

namespace CanopyViewer.Services
{
    public class PasswordService
    {
        public static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool Verify(string password, string hash)
        {
            if (password == null) return false;
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
