using System;
using System.Collections.Generic;
using NotEnoughLogs.Definitions;

namespace NotEnoughLogs.Loggers;

public class MemoryLogger
{
    private readonly List<LogLine> _logs = new();
    public IReadOnlyList<LogLine> Logs => _logs.AsReadOnly();

    private void Log(LogLine line)
    {
        _logs.Add(line);
    }

    public void Log(Enum context, Level level, string message)
    {
        Log(new LogLine
        {
            Level = level,
            Context = context,
            Message = message,
        });
    }

    public void DumpToContainer<TContext>(LoggerContainer<TContext> logger) where TContext : Enum
    {
        foreach (LogLine log in _logs) logger.Log(log);
    }
}