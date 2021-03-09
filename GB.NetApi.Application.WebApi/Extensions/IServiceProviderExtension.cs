using System;

namespace GB.NetApi.Application.WebApi.Extensions
{
    /// <summary>
    /// Extends a <see cref="IServiceProvider"/> implementation
    /// </summary>
    public static class IServiceProviderExtension
    {
        /// <summary>
        /// Try getting a service from the extended <see cref="IServiceProvider"/> implementation
        /// </summary>
        /// <typeparam name="TService">The service type to look for</typeparam>
        /// <param name="provider">The extended <see cref="IServiceProvider"/> implementation where to look for the service</param>
        /// <param name="service">The found service if any</param>
        /// <returns>True if a service has been found, otherwise false</returns>
        public static bool TryGetService<TService>(this IServiceProvider provider, out TService service) where TService : class
        {
            service = provider.GetService(typeof(TService)) as TService;

            return service is not null;
        }
    }
}
