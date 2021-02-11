using GB.NetApi.Application.Services.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Formatters
{
    /// <summary>
    /// Deserialize all entering values in JSON format
    /// </summary>
    public sealed class JsonTextInputFormatter : TextInputFormatter
    {
        #region Fields

        private static readonly JsonTextFormatter Formatter = new JsonTextFormatter();

        #endregion

        public JsonTextInputFormatter()
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            SupportedEncodings.Clear();
            SupportedEncodings.Add(Encoding.UTF8);
        }

        protected override bool CanReadType(Type type) => true;

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var result = await Formatter.DeserializeAsync(context.HttpContext.Request.Body, context.ModelType)
                .ConfigureAwait(false);

            if (result != null)
                return await InputFormatterResult.SuccessAsync(result)
                .ConfigureAwait(false);

            return await InputFormatterResult.FailureAsync()
                .ConfigureAwait(false);
        }
    }
}
