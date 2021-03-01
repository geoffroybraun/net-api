namespace GB.NetApi.Domain.Models.Interfaces.Services
{
    /// <summary>
    /// Represents a service which retrieve a translated message using its key
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// Retrieve a translated message using its key
        /// </summary>
        /// <param name="key">The translated message key to look for</param>
        /// <returns>The found translated message if any, otherwise the key</returns>
        string GetString(string key);

        /// <summary>
        /// Retrieve a formatted translated message using its key
        /// </summary>
        /// <param name="key">The translated message key to look for</param>
        /// <param name="parameters">The parameters to format the translated message with</param>
        /// <returns>The formatted translated message if any, otherwise the key</returns>
        string GetString(string key, params object[] parameters);
    }
}
