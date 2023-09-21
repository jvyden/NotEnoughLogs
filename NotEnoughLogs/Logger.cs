using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NotEnoughLogs.Behaviour;
using NotEnoughLogs.Sinks;

namespace NotEnoughLogs;

public partial class Logger : IDisposable
{
    private readonly LoggerConfiguration _configuration;
    private readonly LoggingBehaviour _behaviour;
    
    public Logger(IEnumerable<ILoggerSink> sinks, LoggerConfiguration? configuration = null)
    {
        configuration ??= LoggerConfiguration.Default;
        _configuration = configuration;
        
        this._behaviour = _configuration.Behaviour;
        this._behaviour.Sinks = sinks.ToList().AsReadOnly();
    }

    public Logger(LoggerConfiguration? configuration = null)
    {
        configuration ??= LoggerConfiguration.Default;
        _configuration = configuration;
        
        ReadOnlyCollection<ILoggerSink> sinks = new List<ILoggerSink>(1)
        {
            new ConsoleSink()
        }.AsReadOnly();
        
        this._behaviour = _configuration.Behaviour;
        this._behaviour.Sinks = sinks;
    }

    private bool CanLog(LogLevel level) => level <= _configuration.MaxLevel;

    public void Log(LogLevel level, Enum category, ReadOnlySpan<char> content)
    {
        if (!CanLog(level)) return;
        this._behaviour.Log(level, category.ToString(), content);
    }

    public void Log(LogLevel level, Enum category, ReadOnlySpan<char> format, params object[] args)
    {
        if (!CanLog(level)) return;
        this._behaviour.Log(level, category.ToString(), format, args);
    }

    public void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> content)
    {
        if (!CanLog(level)) return;
        this._behaviour.Log(level, category, content);
    }

    public void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> format, params object[] args)
    {
        if (!CanLog(level)) return;
        this._behaviour.Log(level, category, format, args);
    }

    public void Dispose()
    {
        if (this._behaviour is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}