using System;
using System.IO;
using System.Threading.Tasks;

namespace GB.NetApi.Domain.Models.Interfaces.Services
{
    /// <summary>
    /// Represents an input text formatter which deserializes a <see cref="Stream"/> to the expected <see cref="Type"/>
    /// </summary>
    public interface ITextInputFormatter
    {
        /// <summary>
        /// Deserializes a <see cref="Stream"/> to the expected <see cref="Type"/>
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to deserialize</param>
        /// <param name="outputType">The <see cref="Type"/> to deserialize the <see cref="Stream"/> to</param>
        /// <returns>The deserialized <see cref="Stream"/> as <see cref="object"/></returns>
        Task<object> DeserializeAsync(Stream stream, Type outputType);
    }
}
