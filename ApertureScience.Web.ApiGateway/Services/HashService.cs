using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;


namespace ApertureScience.Web.ApiGateway.Services
{
    public class HashService
    {
        public delegate byte[] CustomGetBytesDelegate(string text);
        public delegate string CustomSaltDelegate(string text, string salt);

        public static CustomGetBytesDelegate CustomGetBytes;
        public static string HashBinaryFormat = "{0:x2}";
        public static CustomSaltDelegate CustomSalt;

        public static string Hash(string text, string salt, HashTypeEnum hashType)
        {
            text = GetSalt(text, salt);

            switch (hashType)
            {
                case HashTypeEnum.Sha1:
                    text = GetSHA1(text);
                    break;
                case HashTypeEnum.Sha256:
                    text = GetSHA256(text);
                    break;
                case HashTypeEnum.Sha512:
                    text = GetSHA512(text);
                    break;
                default:
                    throw new Exception(String.Format("HashHelper.Hash undefined hash type : {0}", hashType.ToString()));
            }

            return text;
        }
        private static string GetSalt(string text, string salt)
        {
            string textValue = text.ToString().Trim();
            string saltValue = salt.ToString().Trim();
            if (CustomSalt == null)
                return string.Concat(textValue, saltValue);
            else
                return CustomSalt(textValue, saltValue);
        }
        private static string GetSHA1(string text)
        {
            SHA1Managed hashString = new SHA1Managed();
            return HashService.Hash(text, hashString);
        }
        private static string GetSHA256(string text)
        {
            SHA256Managed hashString = new SHA256Managed();
            return HashService.Hash(text, hashString);
        }
        private static string GetSHA512(string text)
        {
            SHA512Managed hashString = new SHA512Managed();
            return HashService.Hash(text, hashString);
        }
        private static string Hash(string text, HashAlgorithm hashString)
        {
            byte[] message = HashService.GetBytes(text);
            return HashService.Hash(message, hashString);
        }
        private static byte[] GetBytes(string text)
        {
            if (CustomGetBytes != null)
            {
                return CustomGetBytes(text);
            }
            else
            {
                UnicodeEncoding UE = new UnicodeEncoding();
                return UE.GetBytes(text);
            }
        }
        private static string Hash(byte[] message, HashAlgorithm hashString)
        {
            byte[] hashValue;
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format(HashService.HashBinaryFormat, x);
            }
            return hex;
        }
    }
}