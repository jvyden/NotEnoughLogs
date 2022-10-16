using System;
using System.Diagnostics;
using NotEnoughLogs.Definitions;

namespace NotEnoughLogs.Extensions {
    internal static class ConsoleColorExtensions {
        internal static ConsoleColor ToDark(this ConsoleColor color) => color switch {
            ConsoleColor.Blue => ConsoleColor.DarkBlue,
            ConsoleColor.Cyan => ConsoleColor.DarkCyan,
            ConsoleColor.Green => ConsoleColor.DarkGreen,
            ConsoleColor.Gray => ConsoleColor.DarkGray,
            ConsoleColor.Magenta => ConsoleColor.DarkMagenta,
            ConsoleColor.Red => ConsoleColor.DarkRed,
            ConsoleColor.White => ConsoleColor.Gray,
            ConsoleColor.Yellow => ConsoleColor.DarkYellow,
            _ => color,
        };

        internal static ConsoleColor ToColor(this Level level) {
            switch(level) {
                case Level.Critical:
                    return ConsoleColor.Yellow;
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
            }
            #if DEBUG
            Debug.Fail("console color out of range");
            #endif
            return ConsoleColor.White;
        }
    }
}