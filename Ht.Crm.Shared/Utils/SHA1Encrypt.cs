using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Abp.Demo.Shared.Utils
{
    /// <summary>
    /// SHA1加密
    /// </summary>
    public class Sha1Encrypt
    {
        /// <summary>
        /// SHA1加密
        /// </summary>        
        public static string GetSha1ToHexString(string input, Encoding encoding)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            byte[] data = sha1.ComputeHash(encoding.GetBytes(input));
            sha1.Clear();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 使用 SHA1_HMAC 进行签名
        /// </summary>
        public static string GetHamcSha1ToHexString(string content, string key, Encoding encoding)
        {
            var hmacshA1 = new HMACSHA1(encoding.GetBytes(key));
            var hash = hmacshA1.ComputeHash(encoding.GetBytes(content));
            hmacshA1.Clear();
            var stringBuilder = new StringBuilder();
            foreach (byte num in hash)
                stringBuilder.Append(num.ToString("x2"));
            return stringBuilder.ToString();
        }
    }
}
