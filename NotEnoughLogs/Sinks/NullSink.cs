using System;
using System.Text;

namespace NotEnoughLogs.Sinks;

public class NullSink : ILoggerSink
{
    public void Log(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> content)
    {
        _ = Encoding.UTF8.GetString(category);
        _ = Encoding.UTF8.GetString(content);
    }

    public void Log(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> format, params object[] args)
    {
        _ = Encoding.UTF8.GetString(category);
        _ = Encoding.UTF8.GetString(format);
    }
}