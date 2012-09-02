namespace ValidatorNET.Expressions
{
    public static class PostalCodes
    {
        public const string France = @"\d{5}";
        public const string Germany = @"(D-)?\d{5}";
        public const string Japan = @"\d{3}(-(\d{4}|\d{2}))?";
        public const string China = @"\d{6}";
        public const string America = @"\d{5}(-\d{4})?";
        public const string Denmark = @"\d{4}";
        public const string India = @"^[1-9]{3}\s{0,1}[0-9]{3}$";
        public const string Swiss = @"^[1-9]{1}[0-9]{3}$";
        public const string Russian = @"\d{6}";
        public const string Australia = @"^(0[289][0-9]{2})|([1345689][0-9]{3})|(2[0-8][0-9]{2})|(290[0-9])|(291[0-4])|(7[0-4][0-9]{2})|(7[8-9][0-9]{2})$";
        public const string Canadia = @"^([A-Za-z]\d[A-Za-z][-|\ ]?\d[A-Za-z]\d)";
        public const string UnitedKingdom = @"^(([A-Z]{1,2}[0-9][0-9A-Z]{0,1})\ ([0-9][A-Z]{2}))|(GIR\ 0AA)$";
        public const string Brazil = @"^\d{5}(-\d{3})?$";
        public const string Dutch = @"^[1-9]{1}[0-9]{3}\s{0,1}?[a-zA-Z]{2}$";
        public const string Invariant = @"\w";
    }
}