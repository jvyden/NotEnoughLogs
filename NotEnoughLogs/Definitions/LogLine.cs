using System;

namespace NotEnoughLogs.Definitions;

public struct LogLine
{
    public Enum Context;
    public Level Level;
    public string Message;

    public LogTrace Trace;
}