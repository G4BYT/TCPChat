﻿using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace Client
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

        /// <summary>
        /// I will highly suggest you to generate a strong passphrase, encode in Base64 and replace this passphrase.
        /// Because the project is open source and everyone has this passphrase! You need to have the same passphrase on client and server.
        /// I am working on implementing a random passphrase every time and sync the passphrase from server to client with RSA. 
        /// But for now, change the passphrase. 
        /// </summary>
        private static readonly string HardCodedPassphrase = "NTQlMjUuNjUlMjgldTAzQTMuc0lQN0lXZUkldTA0MkY=";

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
            Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(Convert.FromBase64String(HardCodedPassphrase), salt, iterations);
            byte[] key = password.GetBytes(16);
            using (ICryptoTransform encryptor = Rijandael.CreateEncryptor(key, IV))
            {
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
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
            Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(Convert.FromBase64String(HardCodedPassphrase), salt, iterations);
            byte[] key = password.GetBytes(16);
            using (ICryptoTransform decryptor = Rijandael.CreateDecryptor(key, IV))
            {
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
        }
    }
}