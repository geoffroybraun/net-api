using GB.NetApi.Application.Services.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Formatters
{
    /// <summary>
    /// Serialize all outering values in JSON format
    /// </summary>
    public sealed class JsonTextOutputFormatter : TextOutputFormatter
    {
        #region Fields

        private static readonly JsonTextFormatter Formatter = new JsonTextFormatter();

        #endregion

        protected override bool CanWriteType(Type type) => true;


        public JsonTextOutputFormatter()
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            SupportedEncodings.Clear();
            SupportedEncodings.Add(Encoding.UTF8);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            if (context.Object is null || context.ObjectType is null)
                return;

            var result = await Formatter.SerializeAsync(context.Object, context.ObjectType)
                .ConfigureAwait(false);
            await context.HttpContext
                .Response
                .Body
                .WriteAsync(selectedEncoding.GetBytes(result))
                .ConfigureAwait(false);
        }
    }
}
