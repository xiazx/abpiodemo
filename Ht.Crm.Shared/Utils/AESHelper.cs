using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Abp.Demo.Shared.Utils
{
    public static class AESHelper
    {
        #region Encryption

        /// <summary>
        /// 加密
        /// </summary>
        public static string EncryptToBase64(string input, string key, string iv)
        {
            return EncryptToBase64(Encoding.UTF8.GetBytes(input), key, iv);
        }
        /// <summary>
        /// 加密
        /// </summary>
        public static string EncryptToBase64(byte[] input, string key, string iv)
        {
            var bytes = EncryptToBytes(input, key, iv);
            return bytes == null ? null : Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// 加密
        /// </summary>
        public static byte[] EncryptToBytes(string input, string key, string iv)
        {
            return EncryptToBytes(Encoding.UTF8.GetBytes(input), key, iv);
        }
        /// <summary>
        /// 加密
        /// </summary>
        public static byte[] EncryptToBytes(byte[] input, string key, string iv)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv); //new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //string str = Encoding.UTF8.GetString(aes.IV);

            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);

            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                    {
                        cs.Write(input, 0, input.Length);
                    }

                    return ms.ToArray();
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Decryption
        /// <summary>
        /// 解密
        /// </summary>
        public static string DecryptFromBase64(string input, string key, string iv)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return string.Empty;
                }

                var bytes = DecryptToBytes(Convert.FromBase64String(input), key, iv);
                if (bytes == null)
                {
                    return string.Empty;
                }
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        public static byte[] DecryptToBytes(byte[] input, string key, string iv)
        {
            var aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);//new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var decrypt = aes.CreateDecryptor();
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    cs.Write(input, 0, input.Length);
                }

                xBuff = ms.ToArray();
            }

            return xBuff;
        }
        #endregion
    }
}
