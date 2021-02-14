using GB.NetApi.Application.WebApi.Formatters;
using GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures;
using GB.NetApi.Infrastructure.Database.Contexts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers
{
    /// <summary>
    /// Represents an abstract test controller which provides useful methods to deriving classes
    /// </summary>
    public abstract class BaseControllerTest<T> : IDisposable where T : class
    {
        #region Fields

        private const string ContentType = "application/json";
        private static readonly Encoding Encoding = Encoding.UTF8;
        private static readonly JsonTextFormatter Formatter = new JsonTextFormatter();
        private bool HasDisposed = false;

        #endregion

        #region Properties

        protected readonly HttpClient BrokenClient;
        protected readonly HttpClient NullClient;
        protected readonly HttpClient Client;

        #endregion

        protected BaseControllerTest(BaseDataFixture<T> fixture)
        {
            if (fixture is null)
                throw new ArgumentNullException(nameof(fixture));

            BrokenClient = InitializeClient(fixture.Broken);
            NullClient = InitializeClient(fixture.Null);
            Client = InitializeClient(fixture.Dummy);
        }

        ~BaseControllerTest() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposing || HasDisposed)
                return;

            BrokenClient.Dispose();
            NullClient.Dispose();
            Client.Dispose();
            HasDisposed = true;
        }

        /// <summary>
        /// Deserialize a response content to a <see cref="T"/> type
        /// </summary>
        /// <typeparam name="T">The type to deserialize the content to</typeparam>
        /// <param name="content">The response content to deserialize</param>
        /// <returns>The deserialized content</returns>
        protected static async Task<T> DeserializeContentAsync<T>(HttpContent content) where T : class
        {
            using var stream = await content.ReadAsStreamAsync().ConfigureAwait(false);
            var result = await Formatter.DeserializeAsync(stream, typeof(T)).ConfigureAwait(false);

            return result as T;
        }

        /// <summary>
        /// Executes a <see cref="HttpMethod.Get"/> request to the provided endpoint
        /// </summary>
        /// <param name="endpoint">The endpoint to request</param>
        /// <returns>The API response</returns>
        protected static async Task<HttpResponseMessage> GetAsync(HttpClient client, string endpoint)
        {
            return await client.GetAsync(endpoint).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes a <see cref="HttpMethod.Post"/> request to the provided endpoint
        /// </summary>
        /// <typeparam name="T">The value type to serialize</typeparam>
        /// <param name="endpoint">The endpoint to request</param>
        /// <param name="value">The value to serialize</param>
        /// <returns>The API response</returns>
        protected static async Task<HttpResponseMessage> PostAsync<T>(HttpClient client, string endpoint, T value)
        {
            var content = await GetStringContentAsync(value).ConfigureAwait(false);
            
            return await client.PostAsync(endpoint, content).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes a <see cref="HttpMethod.Put"/> request to the provided endpoint
        /// </summary>
        /// <typeparam name="T">The value type to serialize</typeparam>
        /// <param name="endpoint">The endpoint to request</param>
        /// <param name="value">The value to serialize</param>
        /// <returns>The API response</returns>
        protected static async Task<HttpResponseMessage> PutAsync<T>(HttpClient client, string endpoint, T value)
        {
            var content = await GetStringContentAsync(value).ConfigureAwait(false);
            
            return await client.PutAsync(endpoint, content).ConfigureAwait(false);
        }

        #region Private methods

        private static HttpClient InitializeClient(T mock)
        {
            var applicationFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(Func<BaseDbContext>));
                        services.AddScoped<Func<BaseDbContext>>((provider) => () => new DummyDbContext());

                        services.RemoveAll(typeof(T));
                        services.AddScoped((provider) => mock);
                    });
                });

            return applicationFactory.CreateClient();
        }

        private static async Task<StringContent> GetStringContentAsync<T>(T value)
        {
            var serialiedValue = await Formatter.SerializeAsync(value, typeof(T)).ConfigureAwait(false);

            return new StringContent(serialiedValue, Encoding, ContentType);
        }

        #endregion
    }
}
