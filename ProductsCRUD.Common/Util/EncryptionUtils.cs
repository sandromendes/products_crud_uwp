using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ProductsCRUD.Common.Util
{
    public static class EncryptionUtils
    {
        private const string SecretKey = "A1B2C3D4E5F6A7B8C9D0E1F2A3B4C5D6E7F8A9B";

        public static string Encrypt(string plainText, string secretKey = SecretKey)
        {
            using (RC2 rc2Alg = RC2.Create())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
                rc2Alg.Key = keyBytes;
                rc2Alg.IV = new byte[8]; // IV deve ser definido para o algoritmo RC2

                ICryptoTransform encryptor = rc2Alg.CreateEncryptor(rc2Alg.Key, rc2Alg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string encryptedText, string secretKey = SecretKey)
        {
            using (RC2 rc2Alg = RC2.Create())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
                rc2Alg.Key = keyBytes;
                rc2Alg.IV = new byte[8]; // IV deve ser definido para o algoritmo RC2

                ICryptoTransform decryptor = rc2Alg.CreateDecryptor(rc2Alg.Key, rc2Alg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedText)))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
