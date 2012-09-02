namespace ValidatorNET.Expressions
{
    public static class PhoneNumbers
    {
        // French phone number. Format: 0x xx xx xx xx | 0xxx xxx xxx | +262 xxx xxx xxx
        public const string France = @"^\+?(?:(?:[\d -]{3})|(?:0\d))(?:[ -]?\d){8,9}$";

        // German phone number. Format: (0xx)xx xx xx | +49 (0) 123 456789
        public const string Germany = @"^(?:[\+][0-9]{1,3}[ \.\-])?(?:[\(]{1}[0-9]{1,6}[\)])?(?:[0-9 \.\-\/]{3,20})(?:(?:x|ext|extension)[ ]?[0-9]{1,4})?$";

        // Japanese phone number. Format: 0x-xxxx-xxxx | 011-81-3-9999-9999 | +81-3-9999-9999
        public const string Japan = @"^\+?(?:\d{1,3}-?\d{1,2}-?\d?|0\d{1,4}|\(0\d{1,4}\))?[ -]?[\d -]{8,14}$";

        // Chinese phone number. Format: (xxx)xxxxxxxx or xxx-xxxxxxxx
        public const string China = @"(\(\d{3}\)|\d{3}-)?\d{8}";

        // US phone number. Format: xxx-xxx-xxxx or (xxx)xxx-xxxx
        public const string America = @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}";

        // India phone number. Format: 0xx - xxxxxxx
        public const string India = @"^(0)?\d[0-9]{2}\s{0,1}[\-]{0,1}\s{0,1}[1-9]{1}\d{6}$";

        // Spain phone number. Format: xxx-xxxxxx
        public const string Spain = @"^\d{2,3}-??\d{6,7}$";

        // United Kingdom phone number. Format: +44 xxx xxx xxx
        public const string UnitedKingdom = @"(((\+44)? ?(\(0\))? ?)|(0))(\ ?\d{3,4}){3}";

        // Brazil phone number. Format: (xx)xxx-xxxx
        public const string Brazil = @"^([0-9]{2})?((\([0-9]{2})\)|[0-9]{2})?([0-9]{3}|[0-9]{4})(\-)?[0-9]{4}$";

        // Dutch phone number. Format: +xx(0)xxxxxxxxx
        public const string Dutch = @"(^\+\d{2}|^\+\d{2}\(0\)|^\(\+\d{2}\)\(0\)|^00\d{2}|^0)(\d{9}$|0-9\-\s]{10}$)";

        // Australian phone number. Format: (0x)xxxxxxxx or xxxxxxxx
        public const string Australia = @"^(\(0\d{1}\))?\d{8}$";

        // Israeli phone number. Format: xxxxxxxxxx or (0x)xxxxxxxx or xxxxxxxx or xxxx xxxx or (0x) xxxx xxxx
        public const string Israel = @"^\d{10}$|^\(0[1-9]{1}\)\d{8}$|^\d{8}$|^\d{4}\ \d{3}\ \d{3}$|^\(0[1-9]{1}\)\ \d{4}\ \d{4}$|^\d{4}\ \d{4}$";

        // New Zealand phone number. Format: (0xx)xxxxxxx
        public const string NewZealand = @"(^\([0]\d{2}\))(\d{6,7}$)";

        // Russian phone number. Format: +7(xxx)xxxxxxx or x-xxx-xxx-xxxx
        public const string Russia = @"((8|\+7)-?)?\(?\d{3}\)?-?\d{1}-?\d{1}-?\d{1}-?\d{1}-?\d{1}-?\d{1}-?\d{1}";

        // Invariant phone number. Format: Almost any
        public const string Invariant = @"^([a-zA-Z,#/ \.\(\)\-\+\*]*[0-9]){7}[0-9a-zA-Z,#/ \.\(\)\-\+\*]*$";

        // Swedish phone number. Format: 0xxx xxxxxx or xxx-xxxxxx
        public const string Sweden = @"^((0)?\d{0,3})[-|\ ]?\d{0,6}$";

        // Italian phone number. Format: +39xxxxxxxxxx or 348xxxxxxx
        public const string Italy = @"^([+]39)?((38[{8,9}|0])|(34[{7-9}|0])|(36[6|8|0])|(33[{3-9}|0])|(32[{8,9}]))([\d]{7})$";

        // Danish phone number. Format: xxxxxxxx
        public const string Denmark = @"^(\d){8}$";
    }
}
