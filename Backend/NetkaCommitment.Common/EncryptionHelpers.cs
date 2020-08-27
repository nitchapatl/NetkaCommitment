using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NetkaCommitment.Common
{
    public static class EncryptionHelpers
    {
        private static string key = "azsxdcfvgbhnjmk,";
        private static string iv = "aw34esdr56tfgy78";

        public static string AESEncrypt(string plainText)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(iv);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Padding = PaddingMode.Zeros;

            // Get an encryptor.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            // Encrypt the data.
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            // Convert the data to a byte array.
            byte[] plainTextBytes;
            plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Write all data to the crypto stream and flush it.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            // Get encrypted array of bytes.
            byte[] cipherTextBytes = memoryStream.ToArray();
            string cipherText = System.Convert.ToBase64String(cipherTextBytes);
            memoryStream.Close();
            cryptoStream.Close();
            return cipherText;
        }

        public static string AESEncrypt(string plainText, string key, string iv)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(iv);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Padding = PaddingMode.Zeros;

            // Get an encryptor.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            // Encrypt the data.
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            // Convert the data to a byte array.
            byte[] plainTextBytes;
            plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Write all data to the crypto stream and flush it.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            // Get encrypted array of bytes.
            byte[] cipherTextBytes = memoryStream.ToArray();
            string cipherText = System.Convert.ToBase64String(cipherTextBytes);
            memoryStream.Close();
            cryptoStream.Close();
            return cipherText;
        }

        public static string AESDecrypt(string cipherText)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(iv);
            byte[] cipherTextBytes = System.Convert.FromBase64String(cipherText);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Padding = PaddingMode.Zeros;

            // Get an decryptor.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

            // Decrypt the data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes;
            plainTextBytes = new byte[cipherTextBytes.Length + 1];

            // Read the data out of the crypto stream.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            memoryStream.Close();
            cryptoStream.Close();

            // Replace null character
            plainText = string.IsNullOrEmpty(plainText) ? "" : plainText;
            return plainText;
        }

        public static string AESDecrypt(string cipherText, string key, string iv)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(iv);
            byte[] cipherTextBytes = System.Convert.FromBase64String(cipherText);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Padding = PaddingMode.Zeros;

            // Get an decryptor.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

            // Decrypt the data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes;
            plainTextBytes = new byte[cipherTextBytes.Length + 1];

            // Read the data out of the crypto stream.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            memoryStream.Close();
            cryptoStream.Close();

            // Replace null character
            plainText = string.IsNullOrEmpty(plainText) ? "" : plainText;
            return plainText;
        }

        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string GenerateString()
        {
            string oKey = "asdhjkl;';lkjhgfdsa[poiuytrewzxcvbnm,.";
            return CreateMD5(string.Format("{0}{1}", oKey, DateTime.Now.ToLongDateString()));
        }

        public static string GeneratePasswordOTP()
        {
            string OTPLength = "4";
            string OTP = string.Empty;

            string Chars = string.Empty;
            Chars = "1,2,3,4,5,6,7,8,9,0";

            char[] seplitChar = { ',' };
            string[] arr = Chars.Split(seplitChar);
            string NewOTP = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(OTPLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                NewOTP += temp;
                OTP = NewOTP;
            }
            return OTP;
        }

    }
}
