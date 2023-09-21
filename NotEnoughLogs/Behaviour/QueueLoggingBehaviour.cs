using System;
using System.Collections.Concurrent;
using System.Threading;

namespace NotEnoughLogs.Behaviour;

/// <summary>
/// The nuclear option. An entire thread is spun up to handle logs to ensure everything is in order.
/// Higher CPU cost, completely prevents multi-threading issues while not blocking.
/// </summary>
public class QueueLoggingBehaviour : LoggingBehaviour
{
    private readonly ConcurrentQueue<(LogLevel level, byte[] category, byte[] format, object[]? args)> _logQueue = new();

    internal override void Initialize()
    {
        Thread thread = new Thread(() =>
        {
            while (true)
            {
                if(!_logQueue.TryDequeue(out (LogLevel level, byte[] category, byte[] format, object[]? args) logLine)) continue;

                if (logLine.args == null) this.LogToSink(logLine.level, logLine.category, logLine.format);
                else this.LogToSink(logLine.level, logLine.category, logLine.format, logLine.args);
            }
        });
        
        thread.Start();
    }

    internal override void Log(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> content)
    {
        this._logQueue.Enqueue((level, category.ToArray(), content.ToArray(), null));
    }

    internal override void Log(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> format, params object[] args)
    {
        this._logQueue.Enqueue((level, category.ToArray(), format.ToArray(), args));
    }
}