namespace GB.NetApi.Domain.Services.Extensions
{
    /// <summary>
    /// Extends the <see cref="string"/> value type
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Indicates if the extended <see cref="string"/> value is not null, nor empty, nor white space
        /// </summary>
        /// <param name="value">The extended <see cref="string"/> value to check</param>
        /// <returns>True if the extended <see cref="string"/> value is not null, nor empty, nor white space, otherwise false</returns>
        public static bool IsNotNullNorEmptyNorWhiteSpace(this string value) => !value.IsNullOrEmptyOrWhiteSpace();

        /// <summary>
        /// Indicates if the extended <see cref="string"/> value is null, empty, or white space
        /// </summary>
        /// <param name="value">The extended <see cref="string"/> value to check</param>
        /// <returns>True if the extended <see cref="string"/> value is null, empty, or white space, otherwise false</returns>
        public static bool IsNullOrEmptyOrWhiteSpace(this string value) => string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
    }
}
