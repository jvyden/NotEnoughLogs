using System;

namespace NotEnoughLogs.Behaviour;

/// <summary>
/// The sinks are called directly upon calling Log.
/// Minimal allocations, but causes multi-threading issues and blocks.
/// </summary>
public class DirectLoggingBehaviour : LoggingBehaviour
{
    internal override void Log(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> content)
    {
        this.LogToSink(level, category, content);
    }

    internal override void Log(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> format, params object[] args)
    {
        this.LogToSink(level, category, format, args);
    }
}