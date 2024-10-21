using System;
using System.Text;
using Konscious.Security.Cryptography;
namespace E_Commerce2Business_V01
{
    public static class PasswordHasher
    {
        // Method to hash the password (unchanged)
        public static string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (var argon2 = new Argon2id(passwordBytes))
            {
                argon2.DegreeOfParallelism = 2; // Number of threads
                argon2.MemorySize = 1024 * 1024; // Memory usage in KB (1 GB)
                argon2.Iterations = 4; // Number of iterations

                byte[] hashBytes = argon2.GetBytes(32); // Generate a 32-byte hash
                return Convert.ToBase64String(hashBytes);
            }
        }

        // Combined method to verify the password
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Convert the input password into a hash
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (var argon2 = new Argon2id(passwordBytes))
            {
                argon2.DegreeOfParallelism = 2; // Use the same parameters as during hashing
                argon2.MemorySize = 1024 * 1024; // Memory usage in KB
                argon2.Iterations = 4; // Number of iterations

                // Generate the hash for comparison
                byte[] hashBytes = argon2.GetBytes(32);
                string hashedInputPassword = Convert.ToBase64String(hashBytes);

                // Compare the generated hash with the stored hash
                return hashedInputPassword == hashedPassword;
            }
        }

    }
}
