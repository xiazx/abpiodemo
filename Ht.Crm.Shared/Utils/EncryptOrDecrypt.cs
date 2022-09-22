using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Abp.Demo.Shared.Utils
{

    /// <summary>
    /// 加密
    /// </summary>
    public class EncryptOrDecrypt
    {
        private static string CONKEY = "3edcVFR$5tgbN";

        private static string ECBCONKEY = "HbD5Ksx8";

        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        static MD5 md5Hash = null;
        static SHA1 sha1Hash = null;

        /// <summary>
        /// 初始化对象
        /// </summary>
        private static void GetHashType()
        {
            if (md5Hash == null)
                md5Hash = MD5.Create();
            if (sha1Hash == null)
                sha1Hash = SHA1.Create();
        }

        /// <summary>
        /// MD5\SHA1加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetHashEncryptionCode(string input, UInt16 type = 1)
        {
            byte[] data = null;
            GetHashType();
            if (type == 1)
                data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            else if (type == 2)
                data = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// MD5\SHA1加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static byte[] GetHashEncryptionCode(byte[] input, UInt16 type = 1)
        {
            byte[] data = null;
            GetHashType();
            if (type == 1)
                data = md5Hash.ComputeHash(input);
            else if (type == 2)
                data = sha1Hash.ComputeHash(input);

            return data;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, CONKEY);
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Decrypt(string Text, string sKey)
        {
            try
            {
                int num = Text.Length / 2;
                byte[] buffer = new byte[num];
                for (int i = 0; i < num; i++)
                {
                    int num3 = Convert.ToInt32(Text.Substring(i * 2, 2), 0x10);
                    buffer[i] = (byte)num3;
                }
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                using (MemoryStream stream = new MemoryStream())
                {
                    byte[] btskeys = Encoding.ASCII.GetBytes(GetHashEncryptionCode(sKey).Substring(0, 8));
                    provider.Key = btskeys;
                    provider.IV = btskeys;

                    CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.FlushFinalBlock();
                    return Encoding.Default.GetString(stream.ToArray());
                }
            }
            catch
            {

            }

            return string.Empty;
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, CONKEY);
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Encrypt(string Text, string sKey)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] bytes = Encoding.Default.GetBytes(Text);
                    byte[] btskeys = Encoding.ASCII.GetBytes(GetHashEncryptionCode(sKey).Substring(0, 8));
                    des.Key = btskeys;
                    des.IV = btskeys;

                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytes, 0, bytes.Length);
                        cs.FlushFinalBlock();
                    }
                    return BitConverter.ToString(ms.ToArray()).Replace("-", "");

                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// ECB编码
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="keystr"></param>
        /// <returns></returns>
        public static byte[] EncryptECB(string encryptString, string keystr = "")
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                using (MemoryStream mStream = new MemoryStream())
                {
                    if (string.IsNullOrEmpty(keystr))
                        keystr = ECBCONKEY;
                    provider.Key = Encoding.UTF8.GetBytes(keystr);
                    provider.IV = Keys;

                    provider.Mode = CipherMode.ECB; //兼容其他语言的Des加密算法  
                    provider.Padding = PaddingMode.Zeros; //自动补0  

                    byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

                    CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(), CryptoStreamMode.Write);
                    cStream.Write(inputByteArray, 0, inputByteArray.Length);
                    cStream.FlushFinalBlock();
                    return mStream.ToArray();
                }
            }
            catch
            {
            }
            return null;
        }

        /// <summary>
        ///  ECB解码
        /// </summary>
        /// <param name="inputByteArray"></param>
        /// <param name="keystr"></param>
        /// <returns></returns>
        public static string DecryptECB(byte[] inputByteArray, string keystr = "")
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                using (MemoryStream mStream = new MemoryStream())
                {
                    if (string.IsNullOrEmpty(keystr))
                        keystr = ECBCONKEY;
                    provider.Key = Encoding.UTF8.GetBytes(keystr);
                    provider.IV = Keys;

                    provider.Mode = CipherMode.ECB; //兼容其他语言的Des加密算法  
                    provider.Padding = PaddingMode.Zeros; //自动补0 

                    CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                    cStream.Write(inputByteArray, 0, inputByteArray.Length);
                    cStream.FlushFinalBlock();
                    return Encoding.UTF8.GetString(mStream.ToArray());
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 3DES加密处理
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Encrypt3DES(string a_strString, string a_strKey)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(a_strKey);
            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(a_strString);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        /// <summary>
        /// 3DES解密处理
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string a_strString, string a_strKey)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(a_strKey);
            DES.Mode = CipherMode.ECB;
            DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            ICryptoTransform DESDecrypt = DES.CreateDecryptor();

            byte[] Buffer = Convert.FromBase64String(a_strString);
            return ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));

        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RSADecrypt(string s, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(key))
            {
                CspParameters cspp = new CspParameters();
                cspp.KeyContainerName = key;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
                rsa.PersistKeyInCsp = true;
                string[] decryptArray = s.Split(new string[] { "-" }, StringSplitOptions.None);
                byte[] decryptByteArray = Array.ConvertAll<string, byte>(decryptArray,
                    (a => Convert.ToByte(byte.Parse(a, System.Globalization.NumberStyles.HexNumber))));
                byte[] bytes = rsa.Decrypt(decryptByteArray, true);
                result = System.Text.UTF8Encoding.UTF8.GetString(bytes);
            }
            return result;
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string s, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(key))
            {
                CspParameters cspp = new CspParameters();
                cspp.KeyContainerName = key;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
                rsa.PersistKeyInCsp = true;
                byte[] bytes = rsa.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(s), true);
                result = BitConverter.ToString(bytes);
            }
            return result;
        }
    }
}
