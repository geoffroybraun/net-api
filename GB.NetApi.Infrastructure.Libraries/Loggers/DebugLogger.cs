using GB.NetApi.Domain.Models.Enums;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace GB.NetApi.Infrastructure.Libraries.Loggers
{
    public sealed class DebugLogger : ILogger
    {
        #region Fields

        private const string DefaultLayout = "{0}|{1}|{2}|{3} > {4}";
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        #endregion

        public void Log(ELogLevel logLevel, string message, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerMemberName = null)
        {
            var callerTypeName = Path.GetFileNameWithoutExtension(callerFilePath);
            message = string.Format(DefaultLayout,
                DateTime.UtcNow.ToString(DateFormat),
                callerTypeName,
                callerMemberName,
                logLevel.ToString().ToUpper(),
                message);

            Debug.WriteLine(message);
        }
    }
}
