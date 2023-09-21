using System;

namespace NotEnoughLogs.Behaviour;

/// <summary>
/// The sinks are called directly upon calling Log.
/// Minimal allocations, but causes multi-threading issues and blocks.
/// </summary>
public class DirectLoggingBehaviour : LoggingBehaviour
{
    internal override void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> content)
    {
        this.LogToSink(level, category, content);
    }

    internal override void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> format, params object[] args)
    {
        this.LogToSink(level, category, format, args);
    }
}