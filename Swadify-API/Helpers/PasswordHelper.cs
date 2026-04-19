namespace Swadify_API.Helpers
{
    public class PasswordHelper
    {
        public static string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password, 12);

        public static bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
