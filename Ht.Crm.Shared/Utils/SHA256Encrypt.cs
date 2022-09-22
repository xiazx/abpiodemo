using System.Security.Cryptography;
using System.Text;

namespace Abp.Demo.Shared.Utils
{
    /// <summary>
    /// SHA1加密
    /// </summary>
    public class Sha256Encrypt
    {
        /// <summary>
        /// SHA256加密
        /// </summary>        
        public static string GetSha256ToHexString(string input)
        {
            return GetSha256ToHexString(input, Encoding.UTF8);
        }
        /// <summary>
        /// SHA256加密
        /// </summary>        
        public static string GetSha256ToHexString(string input, Encoding encoding)
        {
            var sha256 = new SHA256CryptoServiceProvider();
            byte[] data = sha256.ComputeHash(encoding.GetBytes(input));
            sha256.Clear();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
