using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Abp.Demo.Shared.Utils
{
    public static class SecurityHelper
    {
        private static readonly string _StringKey = "Fuckyou!"; //加密所需8位密匙

        #region 加密字符串
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="queryString">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptString(string queryString)
        {
            return Encrypt(queryString, _StringKey);
        }
        #endregion


        #region 解密字符串
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="queryString">需要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptString(string queryString)
        {
            return Decrypt(queryString, _StringKey);
        }
        #endregion

        #region 加密字符串
        /// <summary>
        /// 加密字符串，自定义8位密匙
        /// 加密密匙必须与解密密匙相同
        /// </summary>
        /// <param name="pToEncrypt">需要加密的字符串</param>
        /// <param name="sKey">8位字符密匙</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string pToEncrypt, string sKey)
        {
            var des = new DESCryptoServiceProvider();　//把字符串放到byte数组中

            var inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            des.Key = Encoding.ASCII.GetBytes(sKey);　//建立加密对象的密钥和偏移量
            des.IV = Encoding.ASCII.GetBytes(sKey);　 //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            var ms = new MemoryStream();　　 //使得输入密码必须输入英文文本
            var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            var ret = new StringBuilder();
            foreach (var b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        #endregion

        #region 解密字符串
        /// <summary>
        /// 解密字符串，自定义8位密匙
        /// 加密密匙必须与解密密匙相同
        /// </summary>
        /// <param name="pToDecrypt">需要解密的字符串</param>
        /// <param name="sKey">8位字符密匙</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string pToDecrypt, string sKey)
        {
            var des = new DESCryptoServiceProvider();

            var inputByteArray = new byte[pToDecrypt.Length / 2];
            for (var x = 0; x < pToDecrypt.Length / 2; x++)
            {
                var i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = Encoding.ASCII.GetBytes(sKey);　//建立加密对象的密钥和偏移量，此值重要，不能修改
            des.IV = Encoding.ASCII.GetBytes(sKey);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            var ret = new StringBuilder();　//建立StringBuild对象，createDecrypt使用的是流对象，必须把解密后的文本变成流对象

            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion

        #region 进行DES加密
        /// <summary>
        /// 进行DES加密。
        /// </summary>
        /// <param name="pToEncrypt">要加密的字符串。</param>
        /// <returns>以Base64格式返回的加密字符串。</returns>
        public static string EncryptDes(string pToEncrypt)
        {
            return EncryptDes(pToEncrypt, _StringKey);
        }
        /// <summary>
        /// 进行DES加密。
        /// </summary>
        /// <param name="pToEncrypt">要加密的字符串。</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>以Base64格式返回的加密字符串。</returns>
        public static string EncryptDes(string pToEncrypt, string sKey)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                var inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
                des.Key = Encoding.ASCII.GetBytes(sKey);
                des.IV = Encoding.ASCII.GetBytes(sKey);
                var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                var str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        #endregion 

        #region 进行DES解密
        /// <summary>
        /// 进行DES解密。
        /// </summary>
        /// <param name="pToDecrypt">要解密的以Base64</param>
        /// <returns>已解密的字符串。</returns>
        public static string DecryptDes(string pToDecrypt)
        {
            var inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (var des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.ASCII.GetBytes(_StringKey);
                des.IV = Encoding.ASCII.GetBytes(_StringKey);
                var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                var str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        /// <summary>
        /// 进行DES解密。
        /// </summary>
        /// <param name="pToDecrypt">要解密的以Base64</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>已解密的字符串。</returns>
        public static string DecryptDes(string pToDecrypt, string sKey)
        {
            var inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (var des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.ASCII.GetBytes(sKey);
                des.IV = Encoding.ASCII.GetBytes(sKey);
                var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                var str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        #endregion 


        /// <summary>
        /// 传统MD5加密算法
        /// </summary>
        /// <param name="plainText">原始文字</param>
        /// <returns>加密后的结果</returns>
        public static string Md5EncryptClassic(string plainText)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(plainText))).Replace("-", "").ToLower();
        }

        public static string Md5EncryptUTF8(string plainText)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.UTF8.GetBytes(plainText))).Replace("-", "").ToLower();
        }

        public static string Md5EncryptASCII(string plainText)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.ASCII.GetBytes(plainText))).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 传统MD5加密算法
        /// </summary>
        /// <param name="plainText">原始文字</param>
        /// <returns>加密后的结果</returns>
        public static string Md5EncryptClassic16(string plainText)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(plainText))).Replace("-", "").ToLower().Substring(9, 16);
        }

        //public static unsafe string XMd5EncryptClassic16(string plainText)
        //{
        //    fixed (byte* b = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(plainText)))
        //    {
        //        var bptr = b;
        //        var ch = new char[16];
        //        fixed (char* charr = ch)
        //        {
        //            int curr;
        //            int* tmp;
        //            tmp = &curr;
        //            var chptr = charr;
        //            bptr += 4;
        //            *tmp = bptr[0] % 0x10;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            bptr++;

        //            *tmp = bptr[0] >> 4;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            *tmp = bptr[0] % 0x10;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            bptr++;

        //            *tmp = bptr[0] >> 4;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            *tmp = bptr[0] % 0x10;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            bptr++;

        //            *tmp = bptr[0] >> 4;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            *tmp = bptr[0] % 0x10;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            bptr++;

        //            *tmp = bptr[0] >> 4;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            *tmp = bptr[0] % 0x10;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            bptr++;

        //            *tmp = bptr[0] >> 4;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            *tmp = bptr[0] % 0x10;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            bptr++;

        //            *tmp = bptr[0] >> 4;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            *tmp = bptr[0] % 0x10;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            bptr++;

        //            *tmp = bptr[0] >> 4;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            *tmp = bptr[0] % 0x10;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //            chptr++;
        //            bptr++;


        //            *tmp = bptr[0] >> 4;
        //            chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);

        //            //return new string(charr,0,16);    
        //        }
        //        return new string(ch);

        //    }


        //}


        #region 16进制编码

        ///// <summary>
        ///// 16进制编码
        ///// </summary>
        ///// <param name="bs">输入流</param>
        ///// <returns>16进制小写字符串</returns>
        //public static unsafe string HexEncodingString(byte[] bs)
        //{

        //    var len = bs.Length;
        //    fixed (byte* b = bs)
        //    {
        //        var bptr = b;
        //        var i = 0;
        //        int curr;
        //        var tmp = &curr;
        //        var chs = new char[len * 2];
        //        fixed (char* charr = chs)
        //        {
        //            var chptr = charr;
        //            //int j = 0;
        //            while (i < len)
        //            {
        //                *tmp = bptr[0] & 0xf;
        //                chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //                chptr++;
        //                *tmp = bptr[0] >> 4;
        //                chptr[0] = (char)(*tmp < 10 ? *tmp + 0x30 : *tmp + 0x57);
        //                chptr++;
        //                bptr++;
        //                i++;
        //            }
        //        }
        //        return new string(chs);

        //    }
        //}


        ///// <summary>
        ///// 16进制解码
        ///// </summary>
        ///// <param name="hexStringInput">16进制小写字符串</param>
        ///// <returns>字节码</returns>
        //public static unsafe byte[] DecodeHexEncodingString(string hexStringInput)
        //{
        //    var strLen = hexStringInput.Length;
        //    if (strLen % 2 != 0)
        //        throw new ArgumentOutOfRangeException();
        //    var len = strLen / 2;
        //    fixed (char* charr = hexStringInput)
        //    {
        //        var chptr = charr;
        //        var i = 0;


        //        var bsArr = new byte[len];
        //        fixed (byte* bs = bsArr)
        //        {
        //            var bptr = bs;
        //            int low;
        //            int high;

        //            while (i < len)
        //            {
        //                low = chptr[0];
        //                low = low < 0x3A ? low - 0x30 : low - 0x57;

        //                chptr++;
        //                high = chptr[0];
        //                high = high < 0x3A ? high - 0x30 : high - 0x57;
        //                chptr++;

        //                bptr[0] = (byte)((high << 4) | low);
        //                bptr++;
        //                i++;
        //            }
        //        }
        //        return bsArr;
        //    }
        //}

        #endregion


        #region 异或加密
        //异或加密
        public static string XorPwd(string Pwd)
        {
            if (Pwd == "" || Pwd == null)
            {
                return null;
            }
            var Temp = Pwd.Trim();
            var NewPwd = string.Empty;
            for (var i = 0; i < Temp.Length; i++)
            {
                NewPwd += ((char)((int)Temp[i] ^ i)).ToString();
            }
            return NewPwd;
        }
        #endregion

        #region Base64加密方法
        /// <summary>
        /// 加密Base64字符串,在第三位开始的三位加入了信息
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Encode2Base64String(string str, string encode)
        {
            var bytes = Encoding.GetEncoding(encode).GetBytes(str);

            var strResult = Convert.ToBase64String(bytes);
            return strResult.Insert(3, getDisturbCode());
        }
        /// <summary>
        /// 加密Base64字符串,不加入混淆字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Encode2Base64Str(string str, string encode)
        {
            var bytes = Encoding.GetEncoding(encode).GetBytes(str);

            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string getDisturbCode()
        {
            var result = "";
            var strSeed = "01234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            for (var i = 0; i < 3; i++)
                result += strSeed[random.Next(strSeed.Length)];
            return result;
        }

        /// <summary>
        /// 解码Base64字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="encode">The encode.</param>
        /// <param name="isMixed">if set to <c>true</c> [is mixed].</param>
        /// <returns></returns>
        public static string DecodeBase64String(string str, string encode, bool isMixed)
        {
            if (isMixed)
            {
                str = str.Substring(0, 3) + str.Substring(6);
            }

            var bytes = Convert.FromBase64String(str);

            return Encoding.GetEncoding(encode).GetString(bytes);
        }

        #endregion


        public enum PasswordType
        {
            SHA1,
            MD5
        }

        #region HMAC_SHA1

        public static string Encrypt2HMACSHA1_BS64(string key, string data)
        {
            var hmac = new HMACSHA1();
            hmac.Key = Encoding.UTF8.GetBytes(key);
            var dataBuffer = Encoding.UTF8.GetBytes(data);
            var hashBytes = hmac.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }
        #endregion
    }

}
