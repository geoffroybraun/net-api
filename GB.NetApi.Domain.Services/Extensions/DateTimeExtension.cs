using System;

namespace GB.NetApi.Domain.Services.Extensions
{
    /// <summary>
    /// Extends the <see cref="DateTime"/> value type
    /// </summary>
    internal static class DateTimeExtension
    {
        /// <summary>
        /// Indicates if the extended <see cref="DateTime"/> value is not equal to the other one
        /// </summary>
        /// <param name="value">The extended <see cref="DateTime"/> value to compare to the other one</param>
        /// <param name="other">The other <see cref="DateTime"/> value to compare to the extended one</param>
        /// <returns>True if the extended <see cref="DateTime"/> value is not equal to the other one, otherwise false</returns>
        public static bool IsNotEqualTo(this DateTime value, DateTime other) => value.CompareTo(other) != 0;

        /// <summary>
        /// Indicates if the extended <see cref="DateTime"/> value is superior to the other one
        /// </summary>
        /// <param name="value">The extended <see cref="DateTime"/> value to compare to the other one</param>
        /// <param name="other">The other <see cref="DateTime"/> value to compare to the extended one</param>
        /// <returns>True if the extended <see cref="DateTime"/> value is superior to the other one, otherwise false</returns>
        public static bool IsSuperiorTo(this DateTime value, DateTime other) => value.CompareTo(other) > 0;
    }
}
