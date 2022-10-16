using System;

namespace NotEnoughLogs {
    public abstract class LoggerBase {
        public abstract void Log(LogLine line);
    }
}