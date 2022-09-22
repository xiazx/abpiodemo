using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Abp.Demo.Shared.Utils
{
    public static class RsaUtil
    {
        /// <summary>
        /// 生成密钥
        /// </summary>
        /// <returns></returns>
        public static RSAModel GenerateRSAKey()
        {
            RSAModel rsa = new RSAModel();
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                rsa.PublicKey = Convert.ToBase64String(RSA.ExportCspBlob(false));
                rsa.PrivateKey = Convert.ToBase64String(RSA.ExportCspBlob(true));
            }
            catch
            {
                return null;
            }

            return rsa;
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="plainText">明文，明文不能太长，否则会加密失败</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static string RSAEncrypt(string plainText, string publicKey)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] DataToEncrypt = ByteConverter.GetBytes(plainText);
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                //RSAParameters paraPub = ConvertFromPublicKey(publicKey);
                //RSA.ImportParameters(paraPub);
                RSA.ImportCspBlob(Convert.FromBase64String(publicKey));
                byte[] bytes_Cypher_Text = RSA.Encrypt(DataToEncrypt, false);

                string cypherText = Convert.ToBase64String(bytes_Cypher_Text);
                return cypherText;
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="cypherText">密文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public static string RSADecrypt(string cypherText, string privateKey)
        {
            byte[] DataToDecrypt = Convert.FromBase64String(cypherText);
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                byte[] bytes_Public_Key = Convert.FromBase64String(privateKey);
                RSA.ImportCspBlob(bytes_Public_Key);

                //OAEP padding is only available on Microsoft Windows XP or later. 
                byte[] bytesPlainText = RSA.Decrypt(DataToDecrypt, false);
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                string plainText = ByteConverter.GetString(bytesPlainText);
                return plainText;
            }
            catch (CryptographicException)
            {
                return null;
            }
        }
    }

    public  class RSAModel
    {
        /// <summary>
        /// 公钥
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivateKey { get; set; }
    }

}
