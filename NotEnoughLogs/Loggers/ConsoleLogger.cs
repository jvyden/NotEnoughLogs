using System;

namespace NotEnoughLogs.Loggers {
    public class ConsoleLogger : Logger {
        public override void Log(Log log) {
            Console.WriteLine($"[{log.Area.ToString()}:{log.Level.ToString()}] {log.Message}");
        }
    }
}