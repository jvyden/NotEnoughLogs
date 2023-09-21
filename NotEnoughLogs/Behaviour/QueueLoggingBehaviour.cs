using System;
using System.Collections.Concurrent;
using System.Threading;

namespace NotEnoughLogs.Behaviour;

/// <summary>
/// The nuclear option. An entire thread is spun up to handle logs to ensure everything is in order.
/// Higher CPU cost, completely prevents multi-threading issues while not blocking.
/// </summary>
public class QueueLoggingBehaviour : LoggingBehaviour, IDisposable
{
    private readonly ConcurrentQueue<(LogLevel level, string category, string format, object[]? args)> _logQueue = new();
    private bool _shouldBeRunning = true;

    internal override void Initialize()
    {
        Thread thread = new Thread(() =>
        {
            while (_shouldBeRunning)
            {
                if (!_logQueue.TryDequeue(out (LogLevel level, string category, string format, object[]? args) logLine))
                {
                    Thread.Sleep(25);
                    continue;
                }

                if (logLine.args == null) this.LogToSink(logLine.level, logLine.category, logLine.format);
                else this.LogToSink(logLine.level, logLine.category, logLine.format, logLine.args);
            }
        });
        
        thread.Start();
    }

    internal override void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> content)
    {
        this._logQueue.Enqueue((level, category.ToString(), content.ToString(), null));
    }

    internal override void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> format, params object[] args)
    {
        this._logQueue.Enqueue((level, category.ToString(), format.ToString(), args));
    }

    public void Dispose()
    {
        this._shouldBeRunning = false;

        while (_logQueue.TryDequeue(out (LogLevel level, string category, string format, object[]? args) logLine))
        {
            if (logLine.args == null) this.LogToSink(logLine.level, logLine.category, logLine.format);
            else this.LogToSink(logLine.level, logLine.category, logLine.format, logLine.args);
        }
    }
}