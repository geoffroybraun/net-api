using GB.NetApi.Domain.Models.Enums;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace GB.NetApi.Infrastructure.Libraries.Loggers
{
    /// <summary>
    /// Represents a logger which encapsulates the NLog Nuget package
    /// </summary>
    public sealed class NLogLogger : Domain.Models.Interfaces.Libraries.ILogger
    {
        #region Fields

        private const string DefaultLayout = "${longdate}|${processid}|${threadid}|${logger:shortname=true}|${uppercase:${level}} > ${message}${exception:format=tostring}";
        private const string DefaultDirectory = @".\Logs\";
        private static readonly string ArchivesDirectory = $@"{DefaultDirectory}\Archives\";
        private static readonly IDictionary<string, Logger> Loggers = new Dictionary<string, Logger>();
        private static readonly IDictionary<ELogLevel, LogLevel> MatchingLevels = new Dictionary<ELogLevel, LogLevel>
        {
            { ELogLevel.Error, LogLevel.Error },
            { ELogLevel.Fatal, LogLevel.Fatal },
            { ELogLevel.Information, LogLevel.Info },
            { ELogLevel.Warning, LogLevel.Warn },
        };

        #endregion

        public NLogLogger() => LogManager.Setup().LoadConfiguration(Configure);

        public void Log(ELogLevel logLevel, string message, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerMemberName = null)
        {
            var logger = GetLogger(callerFilePath, callerMemberName);
            var logEvent = new LogEventInfo(MatchingLevels[logLevel], logger.Name, message);
            logger.Log(GetType(), logEvent);
        }

        public void Log(Exception exception, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (exception.InnerException is not null)
                Log(exception.InnerException, callerFilePath, callerMemberName);

            var logger = GetLogger(callerFilePath, callerMemberName);
            logger.Error(exception);
        }

        #region Private methods

        private static Logger GetLogger(string callerFilePath, string callerMemberName)
        {
            var callerTypeName = Path.GetFileNameWithoutExtension(callerFilePath);
            var loggerName = $"{callerTypeName}|{callerMemberName}";

            if (Loggers.TryGetValue(loggerName, out Logger logger))
                return logger;

            logger = LogManager.GetLogger(loggerName);
            Loggers.Add(loggerName, logger);

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
