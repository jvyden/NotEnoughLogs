using System;

namespace NotEnoughLogs.Sinks;

public interface ILoggerSink
{
    void Log(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> content);
    void Log(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> format, params object[] args);
}