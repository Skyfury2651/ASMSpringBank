using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApplication1.Helper
{
    public class PasswordHelper
    {
        public string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public bool ComparePassword(string password, string accountSalt, string accountPasswordHash)
        {
            return CreateMD5(password + accountSalt) == accountPasswordHash;
        }

        // Generate a random string with a given size  
        public string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        //Generate a random array of number with a given size
        public string RandomCode()
        {
            int length = 5;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int shift = Convert.ToInt32(Math.Floor(25 * random.NextDouble()));
                str_build.Append(shift);
            }

            return str_build.ToString();
        }
    }
}