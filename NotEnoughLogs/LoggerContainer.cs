using System;
using System.Collections.Generic;

namespace NotEnoughLogs {
    public class LoggerContainer<TArea> where TArea : Enum {
        private readonly List<Logger<TArea>> loggers = new List<Logger<TArea>>();

        public void Log(TArea area, Level level, string message) {
            foreach(Logger<TArea> logger in this.loggers) {
                 logger.Log(area, level, message);
            }
        }

        public void RegisterLogger(Logger<TArea> logger) {
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