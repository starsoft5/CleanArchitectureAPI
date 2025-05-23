using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateToken(string email, string secretKey, int expireMinutes = 60)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
