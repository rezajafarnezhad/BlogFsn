using System;
using System.Text.RegularExpressions;
using NETCore.Encrypt;

namespace Framework.Infrastructure
{
    public static class StringEx
    {
        public static string AesEncrypt(this string text, string Key)
        {
            if (string.IsNullOrEmpty(Key) || string.IsNullOrWhiteSpace(Key))
                throw new ArgumentNullException("Key Cannot be null.");

            string Encrypt = EncryptProvider.AESEncrypt(text, Key);

            return Encrypt;
        }

        public static string AesDecrypt(this string text, string Key)
        {
            if (string.IsNullOrEmpty(Key) || string.IsNullOrWhiteSpace(Key))
                throw new ArgumentNullException("Key Cannot be null.");

            string Decrypt = EncryptProvider.AESDecrypt(text, Key);

            return Decrypt;
        }

        public static string ToMD5(this string text)
        {
            string Md5Hash = EncryptProvider.Md5(text);
            return Md5Hash;
        }


    }


}