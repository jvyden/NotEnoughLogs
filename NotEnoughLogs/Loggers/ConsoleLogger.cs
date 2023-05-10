using System;
using NotEnoughLogs.Definitions;
using NotEnoughLogs.Extensions;

namespace NotEnoughLogs.Loggers;

public class ConsoleLogger : LoggerBase
{
    public override void Log(LogLine line)
    {
        var timestamp = $"[{DateTime.Now:MM/dd/yy} {DateTime.Now:HH:mm:ss}]";
        var context = $"[{line.Context.ToString()}:{line.Level.ToString()}]";
        var trace = $"<{line.Trace.Name}:{line.Trace.Line}>";

        var oldForeground = Console.ForegroundColor;
        var oldBackground = Console.BackgroundColor;

        Console.ForegroundColor = line.Level.ToColor();

        Console.WriteLine($"{timestamp} {context} {trace} {line.Message}");

        Console.ForegroundColor = oldForeground;
        Console.BackgroundColor = oldBackground;
    }
}