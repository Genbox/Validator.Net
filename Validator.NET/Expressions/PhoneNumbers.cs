namespace ValidatorNET.Expressions
{
    public static class PhoneNumbers
    {
        // French phone number. Format: 09 99 99 99 99 | 0999 999 999 | +262 999 999 999
        public const string France = @"^\+?(?:(?:[\d -]{3})|(?:0\d))(?:[ -]?\d){8,9}$";

        // German phone number. Format: (099)99 99 99 | +49 (0) 123 456789
        public const string Germany = @"^(?:[\+][0-9]{1,3}[ \.\-])?(?:[\(]{1}[0-9]{1,6}[\)])?(?:[0-9 \.\-\/]{3,20})(?:(?:x|ext|extension)[ ]?[0-9]{1,4})?$";

        // Japanese phone number. Format: 09-9999-9999 | 011-81-3-9999-9999 | +81-3-9999-9999
        public const string Japan = @"^\+?(?:\d{1,3}-?\d{1,2}-?\d?|0\d{1,4}|\(0\d{1,4}\))?[ -]?[\d -]{8,14}$";

        // Chinese phone number. Format: (999)99999999 or 999-99999999
        public const string China = @"(\(\d{3}\)|\d{3}-)?\d{8}";

        // US phone number. Format: 1-234-567-8901 91234 | 12345678901 | 1 (234) 567-8901
        public const string America = @"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*(?:[2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|(?:[2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?(?:[2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?(?:[0-9]{4})(?:\s*(?:#|9\.?|e9t\.?|e9tension)\s*(?:\d+))?$";

        // India phone number. Format: 99999-999999 | 9999-9999999
        public const string India = @"^\+?[\d -]{10,}$";

        // Spain phone number. Format: 999999999 | 99-9999999 | 999-999999
        public const string Spain = @"^\d{2,3}[- ]{0,2}\d{6,7}$";

        // United Kingdom phone number. Format: +44 999 999 999
        public const string UnitedKingdom = @"(((\+44)? ?(\(0\))? ?)|(0))(\ ?\d{3,4}){3}";

        // Brazil phone number. Format: (99)999-9999
        public const string Brazil = @"^([0-9]{2})?((\([0-9]{2})\)|[0-9]{2})?([0-9]{3}|[0-9]{4})(\-)?[0-9]{4}$";

        // Dutch phone number. Format: +99(0)999999999
        public const string Dutch = @"(^\+\d{2}|^\+\d{2}\(0\)|^\(\+\d{2}\)\(0\)|^00\d{2}|^0)(\d{9}$|0-9\-\s]{10}$)";

        // Australian phone number. Format: (09)99999999 or 99999999
        public const string Australia = @"^(\(0\d{1}\))?\d{8}$";

        // Israeli phone number. Format: 9999999999 or (09)99999999 or 99999999 or 9999 9999 or (09) 9999 9999
        public const string Israel = @"^\d{10}$|^\(0[1-9]{1}\)\d{8}$|^\d{8}$|^\d{4}\ \d{3}\ \d{3}$|^\(0[1-9]{1}\)\ \d{4}\ \d{4}$|^\d{4}\ \d{4}$";

        // New Zealand phone number. Format: (099)9999999
        public const string NewZealand = @"(^\([0]\d{2}\))(\d{6,7}$)";

        // Russian phone number. Format: +7(999)9999999 or 9-999-999-9999
        public const string Russia = @"((8|\+7)-?)?\(?\d{3}\)?-?\d{1}-?\d{1}-?\d{1}-?\d{1}-?\d{1}-?\d{1}-?\d{1}";

        // Invariant phone number. Format: Almost any
        public const string Invariant = @"^([a-zA-Z,#/ \.\(\)\-\+\*]*[0-9]){7}[0-9a-zA-Z,#/ \.\(\)\-\+\*]*$";

        // Swedish phone number. Format: 0999 999999 or 999-999999
        public const string Sweden = @"^((0)?\d{0,3})[-|\ ]?\d{0,6}$";

        // Italian phone number. Format: +399999999999 or 3489999999
        public const string Italy = @"^([+]39)?((38[{8,9}|0])|(34[{7-9}|0])|(36[6|8|0])|(33[{3-9}|0])|(32[{8,9}]))([\d]{7})$";

        // Danish phone number. Format: 99999999
        public const string Denmark = @"^\+?(?:45)?[\.\- ]?\d{8}$";
    }
}
