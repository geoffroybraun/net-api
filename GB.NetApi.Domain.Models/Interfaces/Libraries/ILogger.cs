using GB.NetApi.Domain.Models.Enums;
using System;
using System.Runtime.CompilerServices;

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
        /// <param name="logLevel">The <see cref="ELogLevel"/> to link the message to</param>
        /// <param name="message">The message to write</param>
        /// <param name="callerFilePath">The file path of the calling class</param>
        /// <param name="callerMemberName">The name of the calling class method</param>
        void Log(ELogLevel logLevel, string message, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerMemberName = null);

        /// <summary>
        /// Writes an exception
        /// </summary>
        /// <param name="exception">The exception to write</param>
        /// <param name="callerFilePath">The file path of the calling class</param>
        /// <param name="callerMemberName">The name of the calling class method</param>
        void Log(Exception exception, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerMemberName = null);
    }
}
