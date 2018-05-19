using System;
using System.Security.Cryptography;
using System.Text;

namespace VidaMais.Protese.Platform.Utility
{
    public class Membership
    {
        public static string GenerateSalt()
        {
            byte[] data = new byte[0x10];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }

        public static string EncodePassword(string pass, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Convert.FromBase64String(salt);
            byte[] inArray = null;

            HashAlgorithm hashAlgorithm = GetHashAlgorithm();
            if (hashAlgorithm is KeyedHashAlgorithm)
            {
                KeyedHashAlgorithm algorithm2 = (KeyedHashAlgorithm)hashAlgorithm;
                if (algorithm2.Key.Length == src.Length)
                {
                    algorithm2.Key = src;
                }
                else if (algorithm2.Key.Length < src.Length)
                {
                    byte[] dst = new byte[algorithm2.Key.Length];
                    Buffer.BlockCopy(src, 0, dst, 0, dst.Length);
                    algorithm2.Key = dst;
                }
                else
                {
                    int num2;
                    byte[] buffer5 = new byte[algorithm2.Key.Length];
                    for (int i = 0; i < buffer5.Length; i += num2)
                    {
                        num2 = Math.Min(src.Length, buffer5.Length - i);
                        Buffer.BlockCopy(src, 0, buffer5, i, num2);
                    }
                    algorithm2.Key = buffer5;
                }
                inArray = algorithm2.ComputeHash(bytes);
            }
            else
            {
                byte[] buffer6 = new byte[src.Length + bytes.Length];
                Buffer.BlockCopy(src, 0, buffer6, 0, src.Length);
                Buffer.BlockCopy(bytes, 0, buffer6, src.Length, bytes.Length);
                inArray = hashAlgorithm.ComputeHash(buffer6);
            }

            return Convert.ToBase64String(inArray);
        }

        internal static HashAlgorithm GetHashAlgorithm()
        {
            return SHA256.Create();
        }

        /// <summary>
        /// Gera um password randomico entre 6 e 10 caracters
        /// </summary>
        /// <returns></returns>
        public static string GeneratePassword()
        {
            const string posibleChars = "abcdefgijkmnopqrstwxyz" + "ABCDEFGIJKMNOPQRSTWXYZ" + "23456789" + "*$-+?_&=!%{}/";

            var password = new StringBuilder();
            var random = new Random();
            //gera um  caracters
            int lenght = random.Next(6, 10);

            for (int i = 0; i < lenght; i++)
            {
                int pos = random.Next(0, posibleChars.Length - 1);
                password.Append(posibleChars[pos]);
            }

            return password.ToString();
        }
    }
}
