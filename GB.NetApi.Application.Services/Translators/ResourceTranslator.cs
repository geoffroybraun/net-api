using GB.NetApi.Domain.Models.Interfaces.Services;
using GB.NetApi.Domain.Services.Extensions;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GB.NetApi.Application.Services.UnitTests")]
namespace GB.NetApi.Application.Services.Translators
{
    /// <summary>
    /// Translates a message using its key from resources files
    /// </summary>
    public sealed class ResourceTranslator : ITranslator
    {
        #region Fields

        private const string DefaultAssemblyName = "GB.NetApi.Domain.Models";
        private static readonly string BaseName = $"{DefaultAssemblyName}.Resources.Resources";
        private readonly CultureInfo Culture;
        private readonly ResourceManager Resource;

        #endregion

        public ResourceTranslator(CultureInfo culture) : this(culture, BaseName, DefaultAssemblyName) { }

        internal ResourceTranslator(CultureInfo culture, string baseName, string assemblyName)
        {
            Culture = culture ?? throw new ArgumentNullException(nameof(culture));
            Resource = new ResourceManager(baseName, Assembly.Load(assemblyName));
        }

        public string GetString(string key)
        {
            if (key.IsNullOrEmptyOrWhiteSpace())
                throw new ArgumentNullException(nameof(key));

            return Resource.GetString(key, Culture);
        }

        public string GetString(string key, params object[] parameters)
        {
            var message = GetString(key);

            if (message.IsNullOrEmptyOrWhiteSpace())
                return default;

            return string.Format(message, parameters);
        }
    }
}
