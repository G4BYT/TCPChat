using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace Admin
{
    public static class Cryptography
    {
        private static readonly int iterations = 1000;
        private static readonly RijndaelManaged Rijandael = new RijndaelManaged()
        {
            BlockSize = 128,
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7
        };

        private static Rfc2898DeriveBytes password;
        private static MemoryStream memoryStream;
        private static CryptoStream cryptoStream;

        /// <summary>
        /// I will highly suggest you to generate a strong passphrase, encode in Base64 and replace this passphrase.
        /// Because the project is open source and everyone has this passphrase! You need to have the same passphrase on admin and server.
        /// I am working on implementing a random passphrase every time and sync the passphrase from server to client with RSA. 
        /// But for now, change the passphrase. 
        /// </summary>
        private static readonly string HardCodedPassphrase = "MDhJJXUwNDIzVCUyNFEldTFFNzI3SSV1MDI0Ny8ldTA0MTclMjVOVDkldTAyNDc=";

        private static byte[] GenerateSecureCharacters()
        {
            byte[] randomBytes = new byte[16];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            return randomBytes;
        }

        public static byte[] Encrypt(string plainText)
        {
            byte[] salt = GenerateSecureCharacters();
            byte[] IV = GenerateSecureCharacters();
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            password = new Rfc2898DeriveBytes(Convert.FromBase64String(HardCodedPassphrase), salt, iterations);
            byte[] key = password.GetBytes(16);
            using (ICryptoTransform encryptor = Rijandael.CreateEncryptor(key, IV))
            {
                memoryStream = new MemoryStream();
                cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = salt;
                cipherTextBytes = cipherTextBytes.Concat(IV).ToArray();
                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetBytes(Convert.ToBase64String(cipherTextBytes));
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            byte[] salt = cipherTextBytesWithSaltAndIv.Take(16).ToArray();
            byte[] IV = cipherTextBytesWithSaltAndIv.Skip(16).Take(16).ToArray();
            byte[] cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip(32).Take(cipherTextBytesWithSaltAndIv.Length - 32).ToArray();
            password = new Rfc2898DeriveBytes(Convert.FromBase64String(HardCodedPassphrase), salt, iterations);
            byte[] key = password.GetBytes(16);
            using (ICryptoTransform decryptor = Rijandael.CreateDecryptor(key, IV))
            {
                memoryStream = new MemoryStream(cipherTextBytes);
                cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
        }
    }
}
