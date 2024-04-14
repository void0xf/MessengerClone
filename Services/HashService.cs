using System.Security.Cryptography;


namespace MessengerClone.Services
{
    interface IHashService
    {
        string StringToHash(string StringToHash, int iterations = 10000);
        bool VerifyString(string String, string Hash);
    }

    public class HashService: IHashService
    { 
    
        public string StringToHash(string StringToHash, int iterations = 10000)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive a key (hash) from the password and salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(StringToHash, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                // Combine the salt and the hash
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                // Convert to base64
                var base64Hash = Convert.ToBase64String(hashBytes);

                return base64Hash;
            }

        }
        public bool VerifyString(string String, string Hash)
        {
            byte[] hashBytes = Convert.FromBase64String(Hash);
            // Get salt
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Compute the hash on the password the user entered
            using (var pbkdf2 = new Rfc2898DeriveBytes(String, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                // Compare the results
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }

        }

    }
}
