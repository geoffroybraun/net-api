using GB.NetApi.Domain.Models.Interfaces.Services;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace GB.NetApi.Application.Services.Formatters
{
    /// <summary>
    /// Deserializes an input text or serializes an output text
    /// </summary>
    public sealed class JsonTextFormatter : ITextInputFormatter, ITextOutputFormatter
    {
        #region Fields

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        #endregion

        public async Task<object> DeserializeAsync(Stream stream, Type outputType)
        {
            var result = await JsonSerializer.DeserializeAsync(stream, outputType, Options)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<string> SerializeAsync(object value, Type inputType)
        {
            var result = JsonSerializer.Serialize(value, inputType, Options);

            return await Task.FromResult(result)
                .ConfigureAwait(false);
        }
    }
}
