using GB.NetApi.Domain.Models.Enums;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.Collections.Generic;

namespace GB.NetApi.Infrastructure.Libraries.Loggers
{
    /// <summary>
    /// Represents a logger which encapsulates the NLog Nuget package
    /// </summary>
    public sealed class NLogLogger : Domain.Models.Interfaces.Libraries.ILogger
    {
        #region Fields

        private const string DefaultLayout = "${longdate}|${logger}|${processid}|${threadid}|${uppercase:${level}} > ${message}${exception:format=tostring}";
        private const string DefaultDirectory = @".\Logs\";
        private static readonly string ArchivesDirectory = $@"{DefaultDirectory}\Archives\";
        private static readonly IDictionary<Type, Logger> Loggers = new Dictionary<Type, Logger>();

        #endregion

        public NLogLogger() => LogManager.Setup().LoadConfiguration(Configure);

        public void Log(Type callingClassType, ELogLevel logLevel, string message)
        {
            var logger = GetLogger(callingClassType);

            switch (logLevel)
            {
                case ELogLevel.Information:
                    logger.Info(message);
                    break;
                case ELogLevel.Warning:
                    logger.Warn(message);
                    break;
                case ELogLevel.Error:
                    logger.Error(message);
                    break;
                case ELogLevel.Fatal:
                    logger.Fatal(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        #region Private methods

        private static Logger GetLogger(Type callingClassType)
        {
            if (Loggers.TryGetValue(callingClassType, out Logger logger))
                return logger;

            logger = LogManager.GetLogger(callingClassType.FullName);
            Loggers.Add(callingClassType, logger);

            return logger;
        }

        private static void Configure(ISetupLoadConfigurationBuilder builder)
        {
            builder.Configuration.Variables.Add("defaultLayout", new SimpleLayout(DefaultLayout));
            builder.Configuration.Variables.Add("defaultDirectory", new SimpleLayout(DefaultDirectory));
            builder.Configuration.Variables.Add("archivesDirectory", new SimpleLayout(ArchivesDirectory));

            var debuggerTarget = new DebuggerTarget("debuggerTarget") { Layout = DefaultLayout };
            builder.Configuration.AddRule(LogLevel.Info, LogLevel.Fatal, debuggerTarget);

            var consoleTarget = new ConsoleTarget("consoleTarget") { Layout = DefaultLayout };
            builder.Configuration.AddRule(LogLevel.Info, LogLevel.Fatal, consoleTarget);

            var defaultFile = new FileTarget("defaultFile")
            {
                ArchiveEvery = FileArchivePeriod.Month,
                ArchiveDateFormat = "yyyy-MM",
                ArchiveFileName = "${var:archivesDirectory}GB.NetApi.{#######}",
                ArchiveNumbering = ArchiveNumberingMode.Date,
                ArchiveOldFileOnStartup = false,
                ConcurrentWrites = true,
                DeleteOldFileOnStartup = false,
                EnableArchiveFileCompression = true,
                FileName = "${var:defaultDirectory}GB.NetApi.${shortdate}.txt",
                KeepFileOpen = false,
                Layout = DefaultLayout,
                MaxArchiveFiles = 12
            };
            builder.Configuration.AddRule(LogLevel.Info, LogLevel.Fatal, defaultFile);
        }

        #endregion
    }
}
