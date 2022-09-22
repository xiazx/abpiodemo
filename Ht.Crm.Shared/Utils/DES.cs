using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Abp.Demo.Shared.Utils
{
    /// <summary>
    /// DES 对称加密
    /// </summary>
    public class DES
    {
        private readonly SymmetricAlgorithm mCSP;

        /// <summary>
        /// 默认实例
        /// </summary>
        public static DES Default { get; set; } = new DES();

        public DES()
        {
            mCSP = new TripleDESCryptoServiceProvider();
            PubKey = "AnTtFdyMY3WHR3VixauLjI5qCKwM2Lzo";
            MixKey = "N7iXt5GcRZI=";
        }

        public DES(string key, string iv)
        {
            mCSP = new TripleDESCryptoServiceProvider();
            PubKey = key;
            MixKey = iv;
        }

        /// <summary>
        /// 加密
        /// </summary>
        public string EncryptString(string value)
        {
            if (value == null) return null;

            var ct = mCSP.CreateEncryptor(Convert.FromBase64String(PubKey), Convert.FromBase64String(MixKey));
            var byt = Encoding.UTF8.GetBytes(value);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                {
                    cs.Write(byt, 0, byt.Length);
                    cs.FlushFinalBlock();
                    var str1 = Convert.ToBase64String(ms.ToArray());
                    str1 = str1.Replace("/", "-").Replace("+", "~");
                    return str1;

                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        public string DecryptString(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            value = value.Replace("-", "/").Replace("~", "+");
            byte[] byt;

            var ct = mCSP.CreateDecryptor(Convert.FromBase64String(PubKey), Convert.FromBase64String(MixKey));
            try
            {
                byt = Convert.FromBase64String(value);
            }
            catch
            {
                return null;
            }

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                {
                    cs.Write(byt, 0, byt.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }

        private string PubKey { get; }

        private string MixKey { get; }
    }
}
