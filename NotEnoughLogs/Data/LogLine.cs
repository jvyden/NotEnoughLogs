using System;

namespace NotEnoughLogs.Data {
    public struct LogLine {
        public Enum Area;
        public Level Level;
        public string Message;

        public LogTrace Trace;
    }
}