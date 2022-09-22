using System.Text.RegularExpressions;

namespace Abp.Demo.Shared.Utils
{
    public class ValidationHelper
    {
        // Taken from W3C as an alternative to the RFC5322 specification: https://html.spec.whatwg.org/#valid-e-mail-address
        // The RFC5322 regex can be found here: https://emailregex.com/
        public static string EmailRegEx { get; set; } = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
        public static string MobileRegEx { get; set; } = @"^1(3\d|4[5-9]|5[0-35-9]|6[567]|7[0-8]|8\d|9[0-35-9])\d{8}$";
        public static bool IsValidEmailAddress(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            return Regex.IsMatch(email, EmailRegEx, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }


        public static bool IsValidMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
            {
                return false;
            }

            return Regex.IsMatch(mobile, MobileRegEx, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
    }
}