using System;
using System.Collections.Generic;
using System.Text;
using static System.Int32;

namespace Abp.Demo.Shared.Utils
{
    /// <summary>
    /// 证件号码解析帮助类
    /// </summary>
    public static class IdCardHelper
    {
        /// <summary>
        /// 解析身份证号码：通过身份证号分析返回年龄和生日
        /// </summary>
        /// <param name="idCard">身份证号，目前只支持一代、二代身份证号</param>
        /// <param name="age">返回年龄</param>
        /// <param name="birth">返回生日</param>
        /// <param name="sex">性别</param>
        /// <returns></returns>
        public static bool AnalysisIdCardNo(this string idCard, out int age, out DateTime birth, out string sex)
        {
            age = 0;
            sex = string.Empty;
            birth = new DateTime(1900, 1, 1);
            try
            {
                if (string.IsNullOrEmpty(idCard))
                {
                    return false;
                }

                idCard = (idCard ?? "").Trim();

                if (idCard.IsIdCardNo())
                {
                    if (idCard.Length < 18) //1代身份证长度处理，组成18位长度，最后一位
                    {
                        idCard = GetFullIdCardNo(idCard.Insert(6, "19"));
                    }

                    birth = new DateTime(Parse(idCard.Substring(6, 4)), Parse(idCard.Substring(10, 2)), Parse(idCard.Substring(12, 2)));

                    age = DateTime.Now.Year - birth.Year - (DateTime.Now.Month > birth.Month ? 0 : (DateTime.Now.Month == birth.Month ? (DateTime.Now.Day >= birth.Day ? 0 : 1) : 1));

                    sex = Parse(idCard.Substring(16, 1)) % 2 == 0 ? "女" : "男";
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是有效身份证号码
        /// </summary>
        public static bool IsIdCardNo(this string idCardNo)
        {
            if (string.IsNullOrWhiteSpace(idCardNo) || (idCardNo.Length != 18 && idCardNo.Length != 15))
            {
                return false;
            }

            var correct = false;
            idCardNo = idCardNo.ToUpper();
            var card15 = @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$";
            var card18 = @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$";
            if (idCardNo.Length == 15)
            {
                correct = System.Text.RegularExpressions.Regex.IsMatch(idCardNo, card15);
            }
            else
            {
                correct = System.Text.RegularExpressions.Regex.IsMatch(idCardNo, card18);
                if (correct)
                {
                    string fullNo = GetFullIdCardNo(idCardNo.Substring(0, 17));
                    correct = string.Equals(fullNo.ToLower(), idCardNo.ToLower(), StringComparison.InvariantCultureIgnoreCase);
                }
            }
            return correct;
        }

        /// <summary>
        /// 通过17位身份证号，补全最后一位验证码，获取完整的18位身份证号码
        /// </summary>
        /// <param name="idCardNo">身份证号前17位</param>
        /// <returns></returns>
        public static string GetFullIdCardNo(this string idCardNo)
        {
            if (string.IsNullOrWhiteSpace(idCardNo))
            {
                return "";
            }
            idCardNo = idCardNo.Trim();

            if (idCardNo.Length != 17)
            {
                return "";
            }

            char[] code = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' }; //最后一位验证码
            int[] weight = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 }; //计算权重

            int result = 0;

            for (int i = 0; i < weight.Length; i++)
            {
                result += Parse(idCardNo[i].ToString()) * weight[i];
            }

            return idCardNo + code[result % 11];
        }
    }
}
