using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
namespace E_Commerce2Business_V01
{
    public static class Helpers
    {
        //genrate otp
        public static int GenerateOTP()
            {
                var randomNumber = new byte[4]; // 4 bytes to generate a sufficiently large number
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber); // Fill the array with secure random bytes
                }
        
                // Convert the random bytes to a number and limit it to 6 digits
                var otp = Math.Abs(BitConverter.ToInt32(randomNumber, 0)) % 1000000;
        
                // Return the OTP as a 6-digit string (e.g., "012345")
                return otp;
            }
        
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
