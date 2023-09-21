using System;
using System.Collections.ObjectModel;
using NotEnoughLogs.Sinks;

namespace NotEnoughLogs.Behaviour;

public abstract class LoggingBehaviour
{
    public ReadOnlyCollection<ILoggerSink> Sinks { get; internal set; }

    internal virtual void Initialize()
    {
        // Do nothing by default
    }

    internal abstract void Log(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> content);
    internal abstract void Log(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> format, params object[] args);

    protected void LogToSink(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> content)
    {
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < this.Sinks.Count; i++)
        {
            ILoggerSink sink = this.Sinks[i];
            sink.Log(level, category, content);
        }
    }
    
    protected void LogToSink(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> format, params object[] args)
    {
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < this.Sinks.Count; i++)
        {
            ILoggerSink sink = this.Sinks[i];
            sink.Log(level, category, format, args);
        }
    }
}