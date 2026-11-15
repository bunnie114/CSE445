
using System;
using System.Security.Cryptography;

namespace PasswordHashLibrary
{
    public class PasswordHasher
    {
        // Hash a password using a random salt.
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password, salt, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                return Convert.ToBase64String(salt) + ":" +
                       Convert.ToBase64String(hash);
            }
        }

        // Check whether a password matches the saved hash.
        public static bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                string[] parts = storedHash.Split(':');

                if (parts.Length != 2)
                    return false;

                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] savedHash = Convert.FromBase64String(parts[1]);

                if (salt.Length != 16 || savedHash.Length != 32)
                    return false;

                using (var pbkdf2 = new Rfc2898DeriveBytes(
                    password, salt, 100000, HashAlgorithmName.SHA256))
                {
                    byte[] newHash = pbkdf2.GetBytes(32);

                    int difference = 0;

                    for (int i = 0; i < 32; i++)
                    {
                        difference |= savedHash[i] ^ newHash[i];
                    }

                    return difference == 0;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
