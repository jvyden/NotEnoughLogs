using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NotEnoughLogs.Definitions;

namespace NotEnoughLogs;

public class LoggerContainer<TContext> : IDisposable where TContext : Enum
{
    private readonly List<LoggerBase> _loggers = new();
    private readonly ConcurrentQueue<LogLine> _logQueue = new();
    private const int LogQueueDelayMs = 20;

    private readonly Task _logQueueTask;
    private bool _stopSignal;
    private readonly int _extraTraceLines;

    public LoggerContainer(int extraTraceLines = 0)
    {
        _logQueueTask = Task.Factory.StartNew(() =>
        {
            while (true)
            {
                if (!_logQueue.TryDequeue(out LogLine line))
                {
                    if (_stopSignal) break;

                    Thread.Sleep(LogQueueDelayMs);
                    continue;
                }

                foreach (LoggerBase logger in _loggers) logger.Log(line);
            }
        });

        _extraTraceLines = extraTraceLines;
    }

    internal void Log(LogLine line)
    {
        _logQueue.Enqueue(line);
    }

    private void Log(Enum context, Level level, string message)
    {
        Log(new LogLine
        {
            Level = level,
            Context = context,
            Message = message,
            Trace = TraceHelper.GetTrace(extraTraceLines: _extraTraceLines)
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