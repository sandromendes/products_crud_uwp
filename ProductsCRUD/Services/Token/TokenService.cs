using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProductsCRUD.Services.Token
{
    public class TokenService : ITokenService
    {
        private const string SecretKey = "5e60a4c65b6dbf8d936173c864865f13bc987f0a7bf8448de0b08a04e1a9ec2d4d72e842035f0cc8c7d9e679b70d42e62b592cee2e5dd8a9b21f9a3ed46d10f04ec8b7d4c7c5769da58d456628e2c1eb81ef7ef8400d2f2e0bb3a1ff90b7b4ea5e9ebc147ccfe7c3f635c1d08df79c7d3984c105ade831db63d0d92cccb0ed869c0fc05d90c613d9d8f2df0d2d4a7ea5c7bce35c6ca0";
        private const string Enterprise = "ENTERPRISENAME";
        private const string Application = "APPLICATIONNAME";

        private const int TOKEN_DURATION_HOURS = 12;

        public string CreateToken(string username, string password)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "AuthenticatedUser"),
                new Claim("Password", password)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Enterprise,
                audience: Application,
                claims: claims,
                expires: DateTime.Now.AddHours(TOKEN_DURATION_HOURS),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey));

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = key,
                    ValidIssuer = Enterprise,
                    ValidAudience = Application,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
