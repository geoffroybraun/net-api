using System.Collections.Generic;
using System.Linq;

namespace GB.NetApi.Domain.Services.Extensions
{
    /// <summary>
    /// Extends an <see cref="IEnumerable{T}"/> implementation
    /// </summary>
    internal static class IEnumerableExtension
    {
        /// <summary>
        /// Indicates if the extended <see cref="IEnumerable{T}"/> implementation is not null nor empty
        /// </summary>
        /// <typeparam name="T">The enumerated type</typeparam>
        /// <param name="enumerable">The extended <see cref="IEnumerable{T}"/> implementation to check</param>
        /// <returns>True if the extended <see cref="IEnumerable{T}"/> implementation is not null nor empty, otherwise false</returns>
        public static bool IsNotNullNorEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.IsNullOrEmpty();

        /// <summary>
        /// Indicates if the extended <see cref="IEnumerable{T}"/> implementation is null or empty
        /// </summary>
        /// <typeparam name="T">The enumerated type</typeparam>
        /// <param name="enumerable">The extended <see cref="IEnumerable{T}"/> implementation to check</param>
        /// <returns>True if the extended <see cref="IEnumerable{T}"/> implementation is null or empty, otherwise false</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => enumerable is null || !enumerable.Any();
    }
}
