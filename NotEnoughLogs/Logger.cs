using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NotEnoughLogs.Behaviour;
using NotEnoughLogs.Sinks;

namespace NotEnoughLogs;

public partial class Logger
{
    private readonly ReadOnlyCollection<ILoggerSink> _sinks;
    private readonly LoggerConfiguration _configuration;
    private readonly LoggingBehaviour _behaviour;
    
    public Logger(IEnumerable<ILoggerSink> sinks, LoggerConfiguration? configuration = null)
    {
        configuration ??= LoggerConfiguration.Default;
        _configuration = configuration;
        
        this._sinks = sinks.ToList().AsReadOnly();
        
        this._behaviour = _configuration.Behaviour;
        this._behaviour.Sinks = _sinks;
    }

    public Logger(LoggerConfiguration? configuration = null)
    {
        configuration ??= LoggerConfiguration.Default;
        _configuration = configuration;
        
        this._sinks = new List<ILoggerSink>
        {
            new ConsoleSink()
        }.AsReadOnly();
        
        this._behaviour = _configuration.Behaviour;
        this._behaviour.Sinks = _sinks;
    }

    public void Log(LogLevel level, Enum category, ReadOnlySpan<char> content)
        => this._behaviour.Log(level, category.ToString(), content);

    public void Log(LogLevel level, Enum category, ReadOnlySpan<char> format, params object[] args)
        => this._behaviour.Log(level, category.ToString(), format, args);

    public void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> content)
        => this._behaviour.Log(level, category, content);

    public void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> format, params object[] args)
        => this._behaviour.Log(level, category, format, args);
}