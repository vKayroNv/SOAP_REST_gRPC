using System.Security.Cryptography;
using System.Text;

namespace WeatherService.Utils
{
    internal static class PasswordUtils
    {
        private const string SecretKey = "KE:^Ud/*bv4swd8_+6eD>8k7A]W*Z$Ac";

        public static (string passwordSalt, string passwordHash) CreatePasswordHash(string password)
        {
            byte[] buffer = RandomNumberGenerator.GetBytes(16);

            string passwordSalt = Convert.ToBase64String(buffer);
            string passwordHash = GetPasswordHash(password, passwordSalt);

            return (passwordSalt, passwordHash);
        }

        public static bool VerifyPassword(string password, string passwordSalt, string passwordHash)
        {
            return GetPasswordHash(password, passwordSalt) == passwordHash;
        }

        public static string GetPasswordHash(string password, string passwordSalt)
        {
            password = $"{password}~{passwordSalt}~{SecretKey}";

            byte[] passwordHash = SHA512.HashData(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(passwordHash);
        }
    }
}
