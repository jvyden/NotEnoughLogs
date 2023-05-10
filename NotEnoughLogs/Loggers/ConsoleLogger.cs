using System;
using NotEnoughLogs.Definitions;
using NotEnoughLogs.Extensions;

namespace NotEnoughLogs.Loggers;

public class ConsoleLogger : LoggerBase
{
    public override void Log(LogLine line)
    {
        string timestamp = $"[{DateTime.Now:MM/dd/yy} {DateTime.Now:HH:mm:ss}]";
        string context = $"[{line.Context.ToString()}:{line.Level.ToString()}]";
        string trace = $"<{line.Trace.Name}:{line.Trace.Line}>";

        ConsoleColor oldForeground = Console.ForegroundColor;
        ConsoleColor oldBackground = Console.BackgroundColor;

        Console.ForegroundColor = line.Level.ToColor();

        Console.WriteLine($"{timestamp} {context} {trace} {line.Message}");

        Console.ForegroundColor = oldForeground;
        Console.BackgroundColor = oldBackground;
    }
}