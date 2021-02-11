using System;
using System.Threading.Tasks;

namespace GB.NetApi.Domain.Models.Interfaces.Services
{
    /// <summary>
    /// Represents an output text formatter which serializes an <see cref="object"/> of an explicit <see cref="Type"/>
    /// </summary>
    public interface ITextOutputFormatter
    {
        /// <summary>
        /// Serializes an <see cref="object"/> of an explicit <see cref="Type"/>
        /// </summary>
        /// <param name="value">The <see cref="object"/> to serialize</param>
        /// <param name="inputType">The explicit <see cref="Type"/> of the provided <see cref="object"/></param>
        /// <returns>The serialized <see cref="object"/></returns>
        Task<string> SerializeAsync(object value, Type inputType);
    }
}
