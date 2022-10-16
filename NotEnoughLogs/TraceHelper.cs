#nullable enable
using System;
using System.Diagnostics;
using System.IO;
using NotEnoughLogs.Data;

namespace NotEnoughLogs {
    internal static class TraceHelper {
        internal const int DefaultDepth = 5;

        internal static LogTrace GetTrace(int depth = DefaultDepth, int extraTraceLines = 0) {
            int skipDepth = depth - 2;
            
            StackTrace trace = new StackTrace(true);
            StackFrame? frame = trace.GetFrame(skipDepth + extraTraceLines);
            if(frame == null) {
                return new LogTrace {
                    Name = string.Empty,
                    Line = string.Empty,
                };
            }

            LogTrace logTrace = new LogTrace();

            string? sourcePath = frame.GetFileName();
            if(sourcePath != null) {
                logTrace.Name = Path.GetFileNameWithoutExtension(sourcePath);
                
                int line = frame.GetFileLineNumber();
                logTrace.Line = line == 0 ? frame.GetMethod().Name : line.ToString();
            }
            else {
                logTrace.Name = frame.GetMethod().DeclaringType.Name;
                logTrace.Line = frame.GetMethod().Name;
            }

            logTrace.Name ??= string.Empty;
            logTrace.Line ??= string.Empty;
            
            return logTrace;
        }
    }
}