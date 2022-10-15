using System;
using System.Collections.Generic;

namespace NotEnoughLogs {
    public class LoggerContainer<TArea> where TArea : Enum {
        private readonly List<Logger> loggers = new List<Logger>();

        public void Log(Log log) {
            foreach(Logger logger in this.loggers) {
                 logger.Log(log);
            }
        }

        public void Log(TArea area, Level level, string message) {
            this.Log(new Log {
                Level = level,
                Area = area,
                Message = message,
            });
        }

        public void RegisterLogger(Logger logger) {
            this.loggers.Add(logger);
        }

        public void LogCritical(TArea area, string message) => this.Log(area, Level.Critical, message);
        public void LogError(TArea area, string message) => this.Log(area, Level.Error, message);
        public void LogWarning(TArea area, string message) => this.Log(area, Level.Warning, message);
        public void LogInfo(TArea area, string message) => this.Log(area, Level.Info, message);
        public void LogDebug(TArea area, string message) => this.Log(area, Level.Debug, message);
        public void LogTrace(TArea area, string message) => this.Log(area, Level.Trace, message);
    }
}