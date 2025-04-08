using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace IssueTrackerPro.Infrastructure.Services.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Şifre boş olamaz!");
            byte[] salt = new byte[16];
            RandomNumberGenerator.Fill(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;

            try
            {
                byte[] hashBytes = Convert.FromBase64String(hash);
                if (hashBytes.Length < 36) // Hash uzunluğunu kontrol et
                    return false;

                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] hashToVerify = pbkdf2.GetBytes(20);
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hashToVerify[i])
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Şifre doğrulama hatası: {ex.Message}");
                return false; // Hata durumunda false dön
            }
        }
    }
}