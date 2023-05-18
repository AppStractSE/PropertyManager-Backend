using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities
{
    public static class AuthUtils
    {
        public const int EXPIRATION_TIME = 5;
        public const int EXPIRATION_DAYS = 30;

        public static JwtSecurityToken GetToken(List<Claim> authClaims, IConfiguration _configuration)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.GetExpirationTime(),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public static DateTime GetExpirationTime(this DateTime dateTime, int alterExpirationTimeInHours = 0)
        {
            return dateTime.AddHours(EXPIRATION_DAYS).AddHours(alterExpirationTimeInHours);
        }

    }
}
