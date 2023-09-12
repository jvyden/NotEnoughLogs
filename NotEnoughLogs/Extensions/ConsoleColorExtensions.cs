using System;
using System.Diagnostics;
using NotEnoughLogs.Definitions;

namespace NotEnoughLogs.Extensions;

internal static class ConsoleColorExtensions
{
    internal static ConsoleColor ToColor(this Level level)
    {
        switch (level)
        {
            case Level.Critical:
            case Level.Error:
                return ConsoleColor.Red;

            case Level.Warning:
                return ConsoleColor.Yellow;

            case Level.Info:
                return ConsoleColor.White;

            case Level.Trace:
                return ConsoleColor.White;
            case Level.Debug:
                return ConsoleColor.Magenta;
            default:
                return ConsoleColor.White;
        }
    }
}