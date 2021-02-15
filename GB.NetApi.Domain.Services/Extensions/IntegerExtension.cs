namespace GB.NetApi.Domain.Services.Extensions
{
    /// <summary>
    /// Extends the <see cref="int"/> value type
    /// </summary>
    public static class IntegerExtension
    {
        /// <summary>
        /// Indicates if the extended <see cref="int"/> value is inferior or equal to the other one
        /// </summary>
        /// <param name="value">The extended <see cref="int"/> value to compare to the other one</param>
        /// <param name="other">The other <see cref="int"/> value to compare to the extended one</param>
        /// <returns>True if the ewtended <see cref="int"/> value is inferior or equal to the other one, otherwise false</returns>
        public static bool IsInferiorOrEqualTo(this int value, int other) => value.CompareTo(other) <= 0;

        /// <summary>
        /// Indicates if the extended <see cref="int"/> value is superior to the other one
        /// </summary>
        /// <param name="value">The extended <see cref="int"/> value to compare to the other one</param>
        /// <param name="other">The other <see cref="int"/> value to compare to the extended one</param>
        /// <returns>True if the extended <see cref="int"/> is superior to the other one, otherwise false</returns>
        public static bool IsSuperiorTo(this int value, int other) => !value.IsInferiorOrEqualTo(other);
    }
}
