using System;

namespace NotEnoughLogs.Sinks;

public interface ILoggerSink
{
    void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> content);
    void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> format, params object[] args);
}