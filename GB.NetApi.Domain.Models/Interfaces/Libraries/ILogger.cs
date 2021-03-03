using GB.NetApi.Domain.Models.Enums;
using System;

namespace GB.NetApi.Domain.Models.Interfaces.Libraries
{
    /// <summary>
    /// Represents a logger which writes a message linked to a level
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes a message linked to a level
        /// </summary>
        /// <param name="callingClassType">The type of the class logging a message</param>
        /// <param name="logLevel">The <see cref="ELogLevel"/> to link the message to</param>
        /// <param name="message">The message to write</param>
        void Log(Type callingClassType, ELogLevel logLevel, string message);
    }
}
