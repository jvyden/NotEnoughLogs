using System;
using System.Collections.Generic;
using NotEnoughLogs.Data;

namespace NotEnoughLogs.Loggers {
    public class MemoryLogger {
        private readonly List<LogLine> logs = new List<LogLine>();
        public IReadOnlyList<LogLine> Logs => this.logs.AsReadOnly();
        
        private void log(LogLine line) {
            this.logs.Add(line);
        }

        public void Log(Enum context, Level level, string message) {
            this.log(new LogLine {
                Level = level,
                Context = context,
                Message = message,
                Trace = TraceHelper.GetTrace(),
            });
        }

        public void DumpToContainer<TContext>(LoggerContainer<TContext> logger) where TContext : Enum {
            foreach(LogLine log in this.logs) {
                logger.Log(log);
            }
        }
    }
}