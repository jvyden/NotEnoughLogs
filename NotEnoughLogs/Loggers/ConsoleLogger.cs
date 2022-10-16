using System;
using NotEnoughLogs.Data;

namespace NotEnoughLogs.Loggers {
    public class ConsoleLogger : LoggerBase {
        public override void Log(LogLine line) {
            Console.WriteLine($"[{line.Area.ToString()}:{line.Level.ToString()}] <{line.Trace.Name}:{line.Trace.Line}> {line.Message}");
        }
    }
}