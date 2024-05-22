using DbMaintenanceWPF.Service.Interface;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DbMaintenanceWPF.Service
{
    class SHA256Helper : ISHA256Helper
    {
        public string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Конвертируем строку пароля и соль в массив байтов
                byte[] bytes = Encoding.UTF8.GetBytes(password + salt);

                // Вычисляем хеш
                byte[] hash = sha256.ComputeHash(bytes);

                // Конвертируем хеш (массив байтов) обратно в строку, чтобы сохранить в базе данных
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    stringBuilder.Append(hash[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
        public string CreateSalt()
        {
            byte[] randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
    }
}
