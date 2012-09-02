namespace ValidatorNET.Enums
{
    /// <summary>
    /// Used to specify the range / length maximum and minimum values
    /// </summary>
    public enum LengthOperator
    {
        /// <summary>
        /// Maximum
        /// </summary>
        LargerThan,
        /// <summary>
        /// Maximum and equals
        /// </summary>
        LargerThanOrEquals,
        /// <summary>
        /// Minimum
        /// </summary>
        LessThan,
        /// <summary>
        /// Minimum and equals
        /// </summary>
        LessThanOrEquals,
        /// <summary>
        /// Equals
        /// </summary>
        Equals
    }
}