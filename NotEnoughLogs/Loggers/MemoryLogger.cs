using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NotEnoughLogs.Data;

namespace NotEnoughLogs.Loggers {
    public class MemoryLogger {
        private readonly List<LogLine> logs = new List<LogLine>();
        public IReadOnlyList<LogLine> Logs => this.logs.AsReadOnly();
        
        private void log(LogLine line) {
            this.logs.Add(line);
        }

        public void Log(Enum area, Level level, string message) {
            this.log(new LogLine {
                Level = level,
                Area = area,
                Message = message,
                Trace = TraceHelper.GetTrace(),
            });
        }

        public void DumpToContainer<TArea>(LoggerContainer<TArea> logger) where TArea : Enum {
            foreach(LogLine log in this.logs) {
                logger.Log(log);
            }
        }
    }
}