using System;
using System.Collections.Generic;

namespace NotEnoughLogs.Loggers {
    public class MemoryLogger {
        private readonly List<Log> logs = new List<Log>();
        public IReadOnlyList<Log> Logs => this.logs.AsReadOnly();
        
        public void Log(Log log) {
            this.logs.Add(log);
        }

        public void Log(Enum area, Level level, string message) {
            this.Log(new Log {
                Level = level,
                Area = area,
                Message = message,
            });
        }

        public void DumpToContainer<TArea>(LoggerContainer<TArea> logger) where TArea : Enum {
            foreach(Log log in this.logs) {
                logger.Log(log);
            }
        }
    }
}