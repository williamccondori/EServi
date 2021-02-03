using System;
using System.Security.Cryptography;

namespace EServi.Shared.Helpers
{
    public static class Encryptor
    {
        // ReSharper disable once InconsistentNaming
        private const int SECRET_SIZE = 40;

        // ReSharper disable once InconsistentNaming
        private const int ITERATIONS_COUNT = 10000;

        public static string GetSecretKey()
        {
            var secretBytes = new byte[SECRET_SIZE];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(secretBytes);
            return Convert.ToBase64String(secretBytes);
        }

        public static string GetHash(string value, string secretKey)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(secretKey), ITERATIONS_COUNT);
            return Convert.ToBase64String(pbkdf2.GetBytes(SECRET_SIZE));
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length + sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}