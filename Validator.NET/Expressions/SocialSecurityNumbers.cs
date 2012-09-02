namespace ValidatorNET.Expressions
{
    public static class SocialSecurityNumbers
    {
        // Chinese ID number. Formar: xxxxxxxxxxxxxxxxxX
        public const string China = @"\d{17}[\d|X]|\d{15}";

        // American SSN. Format: xxxxxxxxx
        public const string America = @"(^|\s)(00[1-9]|0[1-9]0|0[1-9][1-9]|[1-6]\d{2}|7[0-6]\d|77[0-2])(-?|[\. ])([1-9]0|0[1-9]|[1-9][1-9])\3(\d{3}[1-9]|[1-9]\d{3}|\d[1-9]\d{2}|\d{2}[1-9]\d)($|\s|[;:,!\.\?])";

        // Danish SSN. Format: [ddMMyy-xxxx]
        public const string Denmark = @"^((((0[1-9]|[12][0-9]|3[01])(0[13578]|10|12)(\d{2}))|(([0][1-9]|[12][0-9]|30)(0[469]|11)(\d{2}))|((0[1-9]|1[0-9]|2[0-8])(02)(\d{2}))|((29)(02)(00))|((29)(02)([2468][048]))|((29)(02)([13579][26])))[-]\d{4})$";

        // French SSN. Format: xxxxxxxxxxxxxxx or x xx xx xx xxx xxx xx
        public const string French = @"^((\d\ \d{2}\ \d{2}\ \d{2}\ \d{3}\ \d{3}(\ \d{2}|))|(\d\d{2}\d{2}\d{2}\d{3}\d{3}(\d{2}|)))$";
    }
}