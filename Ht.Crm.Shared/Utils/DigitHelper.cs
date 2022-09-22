using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.Demo.Shared.Utils
{
    /// <summary>
    /// 整数工具类
    /// </summary>
    public static class DigitHelper
    {
        private static readonly char[] _digitCharList =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K','L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };



        /// <summary>
        /// long转化为 <paramref name="toBase"/> 进制
        /// </summary>
        public static string H2X(long value, int toBase)
        {
            var digitIndex = 0;
            var longPositive = Math.Abs(value);
            var radix = toBase;
            var outDigits = new char[63];

            for (digitIndex = 0; digitIndex <= 64; digitIndex++)
            {
                if (longPositive == 0) break;

                outDigits[outDigits.Length - digitIndex - 1] =
                    _digitCharList[longPositive % radix];
                longPositive /= radix;
            }

            return new string(outDigits, outDigits.Length - digitIndex, digitIndex);
        }
        
        /// <summary>
        /// 将指定基数的数字的 System.String 表示形式转换为等效的 64 位有符号整数。
        /// </summary>
        /// <param name="value">包含数字的 System.String。</param>
        /// <param name="fromBase">value 中数字的基数，它必须是[2,36]</param>
        /// <returns>等效于 value 中的数字的 64 位有符号整数。- 或 - 如果 value 为 null，则为零。</returns>
        public static long X2H(string value, int fromBase)
        {
            value = value.Trim();
            if (string.IsNullOrEmpty(value)) return 0L;

            var sDigits = new string(_digitCharList, 0, fromBase);
            long result = 0;
            //value = reverse(value).ToUpper(); 1
            value = value.ToUpper(); // 2
            for (var i = 0; i < value.Length; i++)
                if (!sDigits.Contains(value[i].ToString()))
                    throw new ArgumentException(string.Format("The argument \"{0}\" is not in {1} system.", value[i],
                        fromBase));
                else
                    try
                    {
                        //result += (long)Math.Pow(fromBase, i) * getcharindex(_digitCharList, value[i]); 1
                        result += (long) Math.Pow(fromBase, i) *
                                  GetCharIndex(_digitCharList, value[value.Length - i - 1]); //   2
                    }
                    catch
                    {
                        throw new OverflowException("运算溢出.");
                    }

            return result;
        }

        /// <summary>
        /// 任意进制转换,将 <paramref name="fromBase"/> 进制表示 <paramref name="value"/> 的转换为 <paramref name="toBase"/> 进制
        /// </summary>
        /// <param name="value">字符串数值</param>
        /// <param name="fromBase"><paramref name="value"/> 的进制</param>
        /// <param name="toBase">需要转换的进制</param>
        public static string X2X(string value, int fromBase, int toBase)
        {
            if (string.IsNullOrEmpty(value.Trim())) return string.Empty;

            if (fromBase < 2 || fromBase > 36)
                throw new ArgumentException(string.Format("The fromBase radix \"{0}\" is not in the range 2..36.",
                    fromBase));

            if (toBase < 2 || toBase > 36)
                throw new ArgumentException(
                    string.Format("The toBase radix \"{0}\" is not in the range 2..36.", toBase));

            var m = X2H(value, fromBase);
            var r = H2X(m, toBase);
            return r;
        }

        private static int GetCharIndex(char[] arr, char value)
        {
            for (var i = 0; i < arr.Length; i++)
                if (arr[i] == value)
                    return i;
            return 0;
        }



        private static string dec2s2(long val)
        {
            var digits = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var result = new StringBuilder();
            var dec = val;

            do
            {
                result.Insert(0, digits[dec % 62]);
                dec = dec / 62;
            } while (dec != 0);

            return result.ToString();
        }

        //62进制转十进制
        private static long s22dec(string sixty_two)
        {
            var base_map = new Dictionary<char, int>{
                {'0' , 0},
                {'1' , 1},
                {'2' , 2},
                {'3' , 3},
                {'4' , 4},
                {'5' , 5},
                {'6' , 6},
                {'7' , 7},
                {'8' , 8},
                {'9' , 9},
                {'a' , 10},
                {'b' , 11},
                {'c' , 12},
                {'d' , 13},
                {'e' , 14},
                {'f' , 15},
                {'g' , 16},
                {'h' , 17},
                {'i' , 18},
                {'j' , 19},
                {'k' , 20},
                {'l' , 21},
                {'m' , 22},
                {'n' , 23},
                {'o' , 24},
                {'p' , 25},
                {'q' , 26},
                {'r' , 27},
                {'s' , 28},
                {'t' , 29},
                {'u' , 30},
                {'v' , 31},
                {'w' , 32},
                {'x' , 33},
                {'y' , 34},
                {'z' , 35},
                {'A' , 36},
                {'B' , 37},
                {'C' , 38},
                {'D' , 39},
                {'E' , 40},
                {'F' , 41},
                {'G' , 42},
                {'H' , 43},
                {'I' , 44},
                {'J' , 45},
                {'K' , 46},
                {'L' , 47},
                {'M' , 48},
                {'N' , 49},
                {'O' , 50},
                {'P' , 51},
                {'Q' , 52},
                {'R' , 53},
                {'S' , 54},
                {'T' , 55},
                {'U' , 56},
                {'V' , 57},
                {'W' , 58},
                {'X' , 59},
                {'Y' , 60},
                {'Z' , 61},
            };
            var result = 0L;
            var str = sixty_two.ToCharArray();

            for (int i = 0; i < str.Length; i++)
            {
                result *= 62;
                result += base_map[str[i]];
            }

            return result;
        }
    }
}