using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using ValidatorNET.Enums;
using ValidatorNET.Expressions;

namespace ValidatorNET
{
    /// <summary>
    /// Validates data
    /// </summary>
    public static class Validator
    {
        private static string[] _sqlExtremelyDangerousCharacters = { "--", "#" };
        private static string[] _sqlDangerousCharacters = { "'", ";", "=", "&", "<", ">", "*", "\"" };
        private static string[] _sqlExtremelyDangerousStrings = { "drop", "insert", "delete", "truncate", "update", "alter", "exec", "xp_cmdshell" };
        private static string[] _sqlDangerousStrings = { "select", "null", "count", "like", "values", "into" };

        private static string[] _xssExtremelyDangerousCharacters = { "<", ">" };
        private static string[] _xssDangerousCharacters = { "'", ";", "!", "-", "=", "&", "{", "(", ")", "}", "#", "\"" };
        private static string[] _xssExtremelyDangerousStrings = { "fromcharcode", "script", "javascript", "object", ".js", "vbscript", "allowscriptaccess", "activex" };
        private static string[] _xssDangerousStrings = { "iframe", "object", "input", "dynsrc", "lowsrc", "size", "link", "href", "rel", "import", "moz-binding", "htc", "mocha", "livescript", "content", "embed", "src" };
        private static Regex _whiteSpace = new Regex(@"[\c\r\n\t]");
        private static Regex _findMultiSpaces = new Regex(@"[\ ]{2,}", RegexOptions.Multiline);
        private static Regex _findHex = new Regex(@"\&\#x[0-9a-fA-F]{0,3}\;?|\%[0-9a-fA-F]{0,2}\;?");

        #region Common attack checks

        /// <summary>
        /// Checks a string for XSS.
        /// Returns the possibility factor of the string containing a XSS attack.
        /// If it's 71+, then its a potential attack
        /// If it's 201+, then it's a XSS attack.
        /// Most real XSS attacks score from 700 and up
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The posibility factor</returns>
        public static int CheckForXss(string input)
        {
            int weight = 0;

            string temp = NormalizeData(input);

            foreach (string edc in _xssExtremelyDangerousCharacters)
            {
                if (temp.Contains(edc))
                    weight += 200;
            }

            foreach (string dc in _xssDangerousCharacters)
            {
                if (temp.Contains(dc))
                    weight += 35;
            }

            foreach (string eds in _xssExtremelyDangerousStrings)
            {
                if (temp.Contains(eds))
                    weight += 200;
            }

            foreach (string ds in _xssDangerousStrings)
            {
                if (temp.Contains(ds))
                    weight += 100;
            }

            return weight;
        }

        /// <summary>
        /// Checks for SQL injection.
        /// Returns the possibility factor of the string containing a SQL injection attack.
        /// If it's 71+, then its a potential attack
        /// If it's 201+, then it's a SQL injection attack.
        /// Most real SQL injection attacks score from 300 and up
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static int CheckForSqlInjection(string input)
        {
            int weight = 0;

            string temp = NormalizeData(input);

            foreach (string edc in _sqlExtremelyDangerousCharacters)
            {
                if (temp.Contains(edc))
                    weight += 200;
            }

            foreach (string dc in _sqlDangerousCharacters)
            {
                if (temp.Contains(dc))
                    weight += 35;
            }

            foreach (string eds in _sqlExtremelyDangerousStrings)
            {
                if (temp.Contains(eds))
                    weight += 200;
            }

            foreach (string ds in _sqlDangerousStrings)
            {
                if (temp.Contains(ds))
                    weight += 100;
            }

            return weight;
        }

        #endregion

        #region Normalize data and encoding

        /// <summary>
        /// This is only to be used when checking for attacks. It decodes html, removes multispaces, tabs and newlines,
        /// converts hex to ascii and converts unicode to ansi.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string NormalizeData(string data)
        {
            string temp = string.Empty;

            if (!string.IsNullOrEmpty(data))
            {
                //Converts the string to lowercase and decodes the encoded html in the string
                temp = HtmlDecodeCharacters(data.ToLowerInvariant());

                // Finds multispaces and replaces them with a single space
                temp = RemoveMultipleSpaces(temp);

                // Finds tabs, newlines and returns and removes them
                temp = RemoveTabsAndNewLines(temp);

                // Finds any hex in the string and replaces it with ASCII
                temp = ConvertHexToAscii(temp);

                // Converts unicode to ansi characters
                temp = EncodeUTF8ToASCII(temp);
            }
            return temp;
        }

        /// <summary>
        /// Removes several spaces in a row and replaces them with a single space
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Returns the string with removed spaces</returns>
        public static string RemoveMultipleSpaces(string input)
        {
            if (!string.IsNullOrEmpty(input))
                return _findMultiSpaces.Replace(input, " ");

            return string.Empty;
        }

        /// <summary>
        /// Removes tabs and newlines from the input
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string RemoveTabsAndNewLines(string input)
        {
            if (!string.IsNullOrEmpty(input))
                return _whiteSpace.Replace(input, string.Empty);

            return string.Empty;
        }

        /// <summary>
        /// Encodes the ASCII string to UTF8.
        /// </summary>
        /// <param name="asciiInput">The ASCII input.</param>
        /// <returns></returns>
        public static string EncodeASCIToUTF8(string asciiInput)
        {
            if (!string.IsNullOrEmpty(asciiInput))
            {
                byte[] rawBytes = Encoding.ASCII.GetBytes(asciiInput);
                byte[] unicodeBytes = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, rawBytes);

                return Encoding.Unicode.GetString(unicodeBytes);
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts UTF8 string to ASCII
        /// </summary>
        /// <param name="unicodeInput"></param>
        /// <returns></returns>
        public static string EncodeUTF8ToASCII(string unicodeInput)
        {
            if (!string.IsNullOrEmpty(unicodeInput))
            {
                byte[] rawBytes = Encoding.Unicode.GetBytes(unicodeInput);
                byte[] asciiBytes = Encoding.Convert(Encoding.UTF8, Encoding.ASCII, rawBytes);

                return Encoding.ASCII.GetString(asciiBytes);
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts the hex input to a string in ASCII
        /// </summary>
        /// <param name="hexValue">The hex string.</param>
        /// <returns>Returns a ASCII string</returns>
        public static string ConvertHexToAscii(string hexValue)
        {
            if (!string.IsNullOrEmpty(hexValue))
            {
                char[] cleanChars = { '&', '#', 'x', ';', '%' };

                foreach (Match match in _findHex.Matches(hexValue))
                {
                    string matchValueCleaned = match.Value;

                    foreach (char c in cleanChars)
                    {
                        matchValueCleaned = matchValueCleaned.Replace(c, ' ').Trim();
                    }

                    char hexChar = (char)int.Parse(matchValueCleaned, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                    hexValue = hexValue.Replace(match.Value, hexChar.ToString(CultureInfo.InvariantCulture));
                }
            }

            return hexValue;
        }

        /// <summary>
        /// Encode HTML
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A string of encoded characters</returns>
        public static string HtmlEncodeCharacters(string input)
        {
            string encoded = string.Empty;
            if (!string.IsNullOrEmpty(input))
            {
                encoded = HttpUtility.HtmlEncode(input);
            }
            return encoded;
        }

        /// <summary>
        /// Decode HTML
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HtmlDecodeCharacters(string input)
        {
            string decoded = string.Empty;
            if (!string.IsNullOrEmpty(input))
                decoded = HttpUtility.HtmlDecode(input);

            return decoded;
        }

        #endregion

        #region Format Checks

        /// <summary>
        /// Checks an email for the right format
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns true if the email entered is a valid email</returns>
        public static bool CheckEmail(string email)
        {
            Regex emailCheck = new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$");

            return emailCheck.IsMatch(email);
        }

        /// <summary>
        /// Checks a URI for the right format
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>Returns true if it is a valid URI</returns>
        public static bool CheckUri(string uri)
        {
            Regex uriCheck = new Regex(@"(([\w]+:)?//)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?");

            bool regex = uriCheck.IsMatch(uri);
            bool uriType = Uri.IsWellFormedUriString(uri, UriKind.Absolute);

            return regex && uriType;
        }

        /// <summary>
        /// Checks a phonenumber with the specified format.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool CheckPhoneNumber(string phoneNumber, PhoneFormat format)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                switch (format)
                {
                    case PhoneFormat.France:
                        return RegexCheck(phoneNumber, PhoneNumbers.France);
                    case PhoneFormat.Germany:
                        return RegexCheck(phoneNumber, PhoneNumbers.Germany);
                    case PhoneFormat.Japan:
                        return RegexCheck(phoneNumber, PhoneNumbers.Japan);
                    case PhoneFormat.China:
                        return RegexCheck(phoneNumber, PhoneNumbers.China);
                    case PhoneFormat.America:
                        return RegexCheck(phoneNumber, PhoneNumbers.America);
                    case PhoneFormat.India:
                        return RegexCheck(phoneNumber, PhoneNumbers.India);
                    case PhoneFormat.Spain:
                        return RegexCheck(phoneNumber, PhoneNumbers.Spain);
                    case PhoneFormat.UnitedKingdom:
                        return RegexCheck(phoneNumber, PhoneNumbers.UnitedKingdom);
                    case PhoneFormat.Brazil:
                        return RegexCheck(phoneNumber, PhoneNumbers.Brazil);
                    case PhoneFormat.Dutch:
                        return RegexCheck(phoneNumber, PhoneNumbers.Dutch);
                    case PhoneFormat.Australia:
                        return RegexCheck(phoneNumber, PhoneNumbers.Australia);
                    case PhoneFormat.Israel:
                        return RegexCheck(phoneNumber, PhoneNumbers.Israel);
                    case PhoneFormat.NewZealand:
                        return RegexCheck(phoneNumber, PhoneNumbers.NewZealand);
                    case PhoneFormat.Russia:
                        return RegexCheck(phoneNumber, PhoneNumbers.Russia);
                    case PhoneFormat.Invariant:
                        return RegexCheck(phoneNumber, PhoneNumbers.Invariant);
                    case PhoneFormat.Sweden:
                        return RegexCheck(phoneNumber, PhoneNumbers.Sweden);
                    case PhoneFormat.Italy:
                        return RegexCheck(phoneNumber, PhoneNumbers.Italy);
                    case PhoneFormat.Denmark:
                        return RegexCheck(phoneNumber, PhoneNumbers.Denmark);
                    default:
                        return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for postal / zip codes with the specified format
        /// </summary>
        /// <param name="postalCode"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool CheckPostalCode(string postalCode, PostalFormat format)
        {
            if (!string.IsNullOrEmpty(postalCode))
            {
                switch (format)
                {
                    case PostalFormat.France:
                        return RegexCheck(postalCode, PostalCodes.France);
                    case PostalFormat.Germany:
                        return RegexCheck(postalCode, PostalCodes.Germany);
                    case PostalFormat.Japan:
                        return RegexCheck(postalCode, PostalCodes.Japan);
                    case PostalFormat.China:
                        return RegexCheck(postalCode, PostalCodes.China);
                    case PostalFormat.America:
                        return RegexCheck(postalCode, PostalCodes.America);
                    case PostalFormat.Denmark:
                        return RegexCheck(postalCode, PostalCodes.Denmark);
                    case PostalFormat.India:
                        return RegexCheck(postalCode, PostalCodes.India);
                    case PostalFormat.Swiss:
                        return RegexCheck(postalCode, PostalCodes.Swiss);
                    case PostalFormat.Russian:
                        return RegexCheck(postalCode, PostalCodes.Russian);
                    case PostalFormat.Australia:
                        return RegexCheck(postalCode, PostalCodes.Australia);
                    case PostalFormat.Canada:
                        return RegexCheck(postalCode, PostalCodes.Canadia);
                    case PostalFormat.UnitedKingdom:
                        return RegexCheck(postalCode, PostalCodes.UnitedKingdom);
                    case PostalFormat.Brazil:
                        return RegexCheck(postalCode, PostalCodes.Brazil);
                    case PostalFormat.Dutch:
                        return RegexCheck(postalCode, PostalCodes.Dutch);
                    case PostalFormat.Invariant:
                        return RegexCheck(postalCode, PostalCodes.Invariant);
                    default:
                        return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks social security / Id numbers with the specified format
        /// </summary>
        /// <param name="socialSecurityNumber"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool CheckSocialSecurityNumber(string socialSecurityNumber, SocialSecurityNumberFormat format)
        {
            if (!string.IsNullOrEmpty(socialSecurityNumber))
            {
                switch (format)
                {
                    case SocialSecurityNumberFormat.China:
                        return RegexCheck(socialSecurityNumber, SocialSecurityNumbers.China);
                    case SocialSecurityNumberFormat.America:
                        return RegexCheck(socialSecurityNumber, SocialSecurityNumbers.America);
                    case SocialSecurityNumberFormat.Denmark:
                        return RegexCheck(socialSecurityNumber, SocialSecurityNumbers.Denmark);
                    case SocialSecurityNumberFormat.France:
                        return RegexCheck(socialSecurityNumber, SocialSecurityNumbers.French);

                    default:
                        return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Create a regular expression check with the specified pattern
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static bool RegexCheck(string data, string pattern)
        {
            Regex check = new Regex(pattern, RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return check.IsMatch(data);
        }

        /// <summary>
        /// Check the data input if it's a number
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNumeric(string data)
        {
            return data.All(char.IsDigit);
        }

        /// <summary>
        /// Checks the input data if it´s alpha numeric
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(string data)
        {
            return data.All(char.IsLetterOrDigit);
        }

        /// <summary>
        /// Check the input string if it's alpha
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsAlpha(string data)
        {
            return data.All(char.IsLetter);
        }

        #endregion

        #region Length and range checks

        /// <summary>
        /// Checks the length.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="length">The number.</param>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public static bool CheckLength(string input, int length, LengthOperator range)
        {
            if (!string.IsNullOrEmpty(input))
            {
                switch (range)
                {
                    case LengthOperator.LargerThan:
                        if (input.Length > length)
                            return true;
                        break;
                    case LengthOperator.LargerThanOrEquals:
                        if (input.Length >= length)
                            return true;
                        break;
                    case LengthOperator.LessThan:
                        if (input.Length < length)
                            return true;
                        break;
                    case LengthOperator.LessThanOrEquals:
                        if (input.Length <= length)
                            return true;
                        break;
                    case LengthOperator.Equals:
                        if (input.Length == length)
                            return true;
                        break;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks the range.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>Returns true if the specified number is in the range</returns>
        public static bool CheckRange(int number, int min, int max)
        {
            return number > min && number < max;
        }

        /// <summary>
        /// Checks the range.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>Returns true if the specified date is in the range</returns>
        public static bool CheckRange(DateTime date, DateTime min, DateTime max)
        {
            return date > min && date < max;
        }

        /// <summary>
        /// Checks the range.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>Returns true if the specified string length is in the range</returns>
        public static bool CheckRange(string input, int min, int max)
        {
            return input.Length > min && input.Length < max;
        }

        #endregion
    }
}