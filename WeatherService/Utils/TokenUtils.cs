using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WeatherService.Utils
{
    internal static class TokenUtils
    {
        public const string SecretKey = "da.EB9%PZEg(}XSW#HKF7}F8tN+9wB8h";

        public static string GenerateJwtToken(string id)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new()
            {
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[] { new(ClaimTypes.NameIdentifier, id) })
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }
    }
}
