using System;

namespace NotEnoughLogs.Sinks;

public class ConsoleSink : ILoggerSink
{
    public void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> content)
    {
        WriteBracketed(level.ToString());
        Console.Write(' ');
        WriteBracketed(category);
        Console.Write(' ');
        
        Console.Out.WriteLine(content);
    }

    public void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> format, params object[] args)
    {
        this.Log(level, category, string.Format(format.ToString(), args));
    }

    private static void WriteBracketed(ReadOnlySpan<char> span)
    {
        Console.Write('[');
        Console.Out.Write(span);
        Console.Write(']');
    }
}