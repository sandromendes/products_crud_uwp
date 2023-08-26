using System;
using System.Security.Cryptography;
using System.Text;

namespace ProductsCRUD.Services.Token
{
    public class TokenService : ITokenService
    {
        private const string SecretKey = "9A3C1F7D0B6E85214379";

        public string CreateToken(string username, string password)
        {
            string combined = $"{username}:{password}:{SecretKey}";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(combined);
                byte[] hash = sha256.ComputeHash(data);

                return Convert.ToBase64String(hash);
            }
        }

        public bool ValidateToken(string token, string username, string password)
        {
            string expectedToken = CreateToken(username, password);

            return token == expectedToken;
        }
    }
}
