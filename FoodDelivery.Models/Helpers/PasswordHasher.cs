using System.Text;

namespace FoodDelivery.Models.Helpers
{
    public class PasswordHasher
    {
        public static void PasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            { 
                if (computedHash[i] != passwordHash[i]) return false; 
            }

            return true;
        }
    }
}
