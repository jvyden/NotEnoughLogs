using System;

namespace NotEnoughLogs.Loggers {
    public class ConsoleLogger<TArea> : Logger<TArea> where TArea : Enum {
        internal override void Log(TArea area, Level level, string message) {
            Console.WriteLine($"[{area.ToString()}:{level.ToString()}] {message}");
        }
    }
}