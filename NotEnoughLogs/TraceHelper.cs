#nullable enable
using System.Diagnostics;
using System.IO;
using NotEnoughLogs.Definitions;

namespace NotEnoughLogs;

internal static class TraceHelper
{
    private const int DefaultDepth = 5;

    internal static LogTrace GetTrace(int depth = DefaultDepth, int extraTraceLines = 0)
    {
        var skipDepth = depth - 2;

        var trace = new StackTrace(true);
        var frame = trace.GetFrame(skipDepth + extraTraceLines);
        if (frame == null)
            return new LogTrace
            {
                Name = string.Empty,
                Line = string.Empty
            };

        var logTrace = new LogTrace();

        var sourcePath = frame.GetFileName();
        if (sourcePath != null)
        {
            logTrace.Name = Path.GetFileNameWithoutExtension(sourcePath);

            var line = frame.GetFileLineNumber();
            logTrace.Line = line == 0 ? frame.GetMethod().Name : line.ToString();
        }
        else
        {
            logTrace.Name = frame.GetMethod().DeclaringType.Name;
            logTrace.Line = frame.GetMethod().Name;
        }

        logTrace.Name ??= string.Empty;
        logTrace.Line ??= string.Empty;

        return logTrace;
    }
}