namespace GB.NetApi.Domain.Services.Extensions
{
    /// <summary>
    /// Extends the <see cref="double"/> value type
    /// </summary>
    internal static class DoubleExtension
    {
        /// <summary>
        /// Indicates if the extended <see cref="double"/> vlaue is superior to the other one
        /// </summary>
        /// <param name="value">The extended <see cref="double"/> value to compare to the other one</param>
        /// <param name="other">The other <see cref="double"/> value to compare to the extended one</param>
        /// <returns>True if the extended <see cref="double"/> value is superiro to the other one, otherwise false</returns>
        public static bool IsSuperiorTo(this double value, double other) => value.CompareTo(other) > 0;
    }
}
