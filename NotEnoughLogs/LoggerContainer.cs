using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotEnoughLogs.Definitions;

namespace NotEnoughLogs;

public class LoggerContainer<TContext> : IDisposable where TContext : Enum
{
    private readonly List<LoggerBase> _loggers = new();
    private readonly ConcurrentQueue<LogLine> _logQueue = new();
    private const int LogQueueDelayMs = 5;

    private readonly Task _logQueueTask;
    private bool _stopSignal;

    public LoggerContainer()
    {
        _logQueueTask = Task.Factory.StartNew(async () =>
        {
            while (true)
            {
                if (!_logQueue.TryDequeue(out LogLine line))
                {
                    if (_stopSignal) break;

                    await Task.Delay(LogQueueDelayMs);
                    continue;
                }

                foreach (LoggerBase logger in _loggers) logger.Log(line);
            }
        });
    }

    internal void Log(LogLine line)
    {
        _logQueue.Enqueue(line);
    }

    private void Log(Enum context, Level level, string message)
    {
        // Copy the message, as we bring it to another thread where it could be garbage collected by the time it's used
        // Should resolve some weird issues
        string messageCopy = string.Copy(message);

        Log(new LogLine
        {
            Level = level,
            Context = context,
            Message = messageCopy,
        });
    }

    public void RegisterLogger(LoggerBase logger)
    {
        _loggers.Add(logger);
    }

    public void LogCritical(TContext context, string message)
    {
        Log(context, Level.Critical, message);
    }

    public void LogError(TContext context, string message)
    {
        Log(context, Level.Error, message);
    }

    public void LogWarning(TContext context, string message)
    {
        Log(context, Level.Warning, message);
    }

    public void LogInfo(TContext context, string message)
    {
        Log(context, Level.Info, message);
    }

    public void LogDebug(TContext context, string message)
    {
        Log(context, Level.Debug, message);
    }

    public void LogTrace(TContext context, string message)
    {
        Log(context, Level.Trace, message);
    }

    public void Dispose()
    {
        _stopSignal = true;
        _logQueueTask.Wait();
        _logQueueTask.Dispose();
    }
}