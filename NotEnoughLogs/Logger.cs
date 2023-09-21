using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NotEnoughLogs.Behaviour;
using NotEnoughLogs.Sinks;

namespace NotEnoughLogs;

public class Logger
{
    private readonly ReadOnlyCollection<ILoggerSink> _sinks;
    private readonly LoggerConfiguration _configuration;
    
    private readonly Lazy<ConcurrentQueue<(LogLevel level, string content, string format, object[]? args)>> _logQueue = new();
    
    public Logger(IEnumerable<ILoggerSink> sinks, LoggerConfiguration? configuration = null)
    {
        configuration ??= LoggerConfiguration.Default;
        _configuration = configuration;
        
        this._sinks = sinks.ToList().AsReadOnly();
    }

    public Logger(LoggerConfiguration? configuration = null)
    {
        configuration ??= LoggerConfiguration.Default;
        _configuration = configuration;
        
        this._sinks = new List<ILoggerSink>
        {
            new ConsoleSink()
        }.AsReadOnly();
    }

    private void LogToSink(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> content)
    {
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < _sinks.Count; i++)
        {
            ILoggerSink sink = _sinks[i];
            sink.Log(level, category, content);
        }
    }
    
    private void LogToSink(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> format, params object[] args)
    {
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < _sinks.Count; i++)
        {
            ILoggerSink sink = _sinks[i];
            sink.Log(level, category, format, args);
        }
    }

    public void Log(LogLevel level, Enum category, ReadOnlySpan<char> content) 
        => this.Log(level, category.ToString(), content);

    public void Log(LogLevel level, Enum category, ReadOnlySpan<char> format, params object[] args) 
        => this.Log(level, category.ToString(), format, args);

    public void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> content)
    {
        switch (this._configuration.Behaviour)
        {
            case LoggingBehaviour.Direct:
                this.LogToSink(level, category, content);
                break;
            case LoggingBehaviour.Queue:
            {
                // allocate the strings here since we pass off to the queue
                _logQueue.Value.Enqueue((level, category.ToString(), content.ToString(), null));
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> format, params object[] args)
    {

    }
}