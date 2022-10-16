using System;
using System.Collections.Generic;

namespace NotEnoughLogs.Loggers {
    public class MemoryLogger {
        private readonly List<LogLine> logs = new List<LogLine>();
        public IReadOnlyList<LogLine> Logs => this.logs.AsReadOnly();
        
        public void Log(LogLine line) {
            this.logs.Add(line);
        }

        public void Log(Enum area, Level level, string message) {
            this.Log(new LogLine {
                Level = level,
                Area = area,
                Message = message,
            });
        }

        public void DumpToContainer<TArea>(LoggerContainer<TArea> logger) where TArea : Enum {
            foreach(LogLine log in this.logs) {
                logger.Log(log);
            }
        }
    }
}