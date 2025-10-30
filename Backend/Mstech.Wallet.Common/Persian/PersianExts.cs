using Common.Gaurd;
using DNTPersianUtils.Core;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Common.Persian
{
    public static class PersianExts
    {
        private const string _IRAN_MOBILE_PREFIX = "98";
        private static readonly System.Type _typeOfString = typeof(string);

        public const string _NUMS = "0123456789";
        public const string _UPPERS = "ABCDEFGHIGKLMNOPQRSTUVWXYZ";
        public const string _LOWERS = "abcdefghijklmnopqrstuvwxyz";
        public const string _ALPHANUMERICS = _NUMS + _UPPERS + _LOWERS;
        public const string _EMAIL_PATTERN = @"^((([a-zA-Z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-zA-Z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
        public const string _PHONE_PATTERN = @"(?<Code>\+98|0|98)?(?<Number>9[0123456789]{9})";

        public static string DeNormalizePhoneNumber(this string input) {
            string result = input;
            if (input.StartsWith("98") && input.Length > 10) {
                result = "0" + result.TrimStart('9').TrimStart('8');
            }
            return result;
        }

        public static string NormalizePhoneNumber(this string input) {
            if (input.IsNullOrEmpty())
                return null;

            if (input.StartsWith('+'))
                return input.TrimStart('+');

            if (input.StartsWith("09"))
                return $"{_IRAN_MOBILE_PREFIX}{input.TrimStart('0')}";

            return input;
        }

        public static bool IsPhoneNumber(this string input)
            => input.IsValidIranianMobileNumber();

        public static bool IsShebaNoValid(this string input) {
            if (input.IsNullOrEmpty())
                return false;

            // https://fa.wikipedia.org/wiki/%D8%A7%D9%84%DA%AF%D9%88%D8%B1%DB%8C%D8%AA%D9%85_%DA%A9%D8%AF_%D8%B4%D8%A8%D8%A7

            if (input.Length != 26)
                return false;

            var ibanCharNumber = new Dictionary<char, int>
            {
                { 'A', 10 },
                { 'B', 11 },
                { 'C', 12 },
                { 'D', 13 },
                { 'E', 14 },
                { 'F', 15 },
                { 'G', 16 },
                { 'H', 17 },
                { 'I', 18 },
                { 'J', 19 },
                { 'K', 20 },
                { 'L', 21 },
                { 'M', 22 },
                { 'N', 23 },
                { 'O', 24 },
                { 'P', 25 },
                { 'Q', 26 },
                { 'R', 27 },
                { 'S', 28 },
                { 'T', 29 },
                { 'U', 30 },
                { 'V', 31 },
                { 'W', 32 },
                { 'X', 33 },
                { 'Y', 34 },
                { 'Z', 35 }
            };

            var result = Regex.Match(input, @"(?<Prefix>[A-Z]{2})(?<PrefixNumber>\d{2})(?<Code>\d{22})");

            if (!result.Success)
                return false;

            var prefix = result.Groups["Prefix"].Value.Trim();
            var prefixNumber = result.Groups["PrefixNumber"].Value.Trim();
            var code = result.Groups["Code"].Value.Trim();

            var prefixAsNumber = prefix.Aggregate(string.Empty, (r, current) => $"{r}{ibanCharNumber[current]}");

            return decimal.Parse($"{code}{prefixAsNumber}{prefixNumber}") % 97 == 1;
        }

        public static bool IsNationalCodeValid(this string input) {
            // http://www.aliarash.com/article/codemeli/codemeli.htm

            if (input.IsNullOrEmpty() || input.Length != 10)
                return false;

            var mod = input.Take(9)
                .Select((current, index) => CharUnicodeInfo.GetDecimalDigitValue(current) * (10 - index))
                .Sum() % 11;

            var ctrl = CharUnicodeInfo.GetDecimalDigitValue(
                input.Skip(9).First()
            );

            return mod < 2 ? ctrl == mod : ctrl == 11 - mod;
        }

        public static string ToLocalPhoneNumber(this string input) {
            if (input.IsNullOrEmpty())
                return null;

            var regexMatch = Regex.Match(input, _PHONE_PATTERN);
            if (!regexMatch.Success)
                return null;

            var number = regexMatch.Groups["Number"].Value.Trim();

            return $"0{number}";
        }

    }
}
