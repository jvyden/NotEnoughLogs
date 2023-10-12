using System;

namespace NotEnoughLogs.Sinks;

public class ConsoleSink : ILoggerSink
{
    public void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> content)
    {
        lock (Console.Out)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = LogColor(level);
        
            DateTime time = DateTime.Now;
            WriteBracketed($"{time:MM/dd/yy} {time:HH:mm:ss}");
            Console.Write(' ');
            WriteBracketed(level.ToString());
            Console.Write(' ');
            WriteBracketed(category);
            Console.Write(' ');
        
            Console.Out.WriteLine(content);
            Console.ForegroundColor = oldColor;
        }
    }

    public void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> format, params object[] args)
    {
        this.Log(level, category, string.Format(format.ToString(), args));
    }
    
    private static void WriteBracketed(string str)
    {
        Console.Write('[');
        Console.Write(str);
        Console.Write(']');
    }

    private static void WriteBracketed(ReadOnlySpan<char> span)
    {
        Console.Write('[');
        Console.Out.Write(span);
        Console.Write(']');
    }
    
    private static ConsoleColor LogColor(LogLevel level)
    {
        switch (level)
        {
            case LogLevel.Critical:
            case LogLevel.Error:
                return ConsoleColor.Red;

            case LogLevel.Warning:
                return ConsoleColor.Yellow;

            case LogLevel.Info:
                return ConsoleColor.White;

            case LogLevel.Trace:
                return ConsoleColor.White;
            case LogLevel.Debug:
                return ConsoleColor.Magenta;
            default:
                return ConsoleColor.White;
        }
    }
}