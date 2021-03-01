using GB.NetApi.Domain.Services.Extensions;
using System.Collections.Generic;

namespace GB.NetApi.Application.Services.Collectors
{
    /// <summary>
    /// Represents a collector which stores messages and retrieves them
    /// </summary>
    public sealed class MessagesCollector
    {
        #region Fields

        private readonly List<string> CollectedMessages = new List<string>();

        #endregion

        #region Properties

        public IEnumerable<string> Messages => CollectedMessages;

        #endregion

        /// <summary>
        /// Stores q message to retrieve it later
        /// </summary>
        /// <param name="message">The mesage to collect</param>>
        public void Collect(string message)
        {
            if (message.IsNullOrEmptyOrWhiteSpace())
                return;

            CollectedMessages.Add(message);
        }
    }
}
