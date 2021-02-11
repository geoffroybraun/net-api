using System.Collections.Generic;
using System.Linq;

namespace GB.NetApi.Domain.Services.Extensions
{
    /// <summary>
    /// Extends an <see cref="IEnumerable{T}"/> implementation
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// Indicates if the extended <see cref="IEnumerable{T}"/> implementation is not null nor empty
        /// </summary>
        /// <typeparam name="T">The enumerated type</typeparam>
        /// <param name="enumerable">The extended <see cref="IEnumerable{T}"/> implementation to check</param>
        /// <returns>True if the extended <see cref="IEnumerable{T}"/> implementation is not null nor empty, otherwise false</returns>
        public static bool IsNotNullNorEmpty<T>(this IEnumerable<T> enumerable) => enumerable is not null && enumerable.Any();
    }
}
