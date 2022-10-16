using System;

namespace NotEnoughLogs.Data {
    public struct LogLine {
        public Enum Context;
        public Level Level;
        public string Message;

        public LogTrace Trace;
    }
}