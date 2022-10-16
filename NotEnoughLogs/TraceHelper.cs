using System;
using System.IO;
using System.Linq;
using NotEnoughLogs.Data;

namespace NotEnoughLogs {
    internal static class TraceHelper {
        internal const int DefaultDepth = 5;
        internal const int SkipDepth = DefaultDepth - 2;
        
        internal static LogTrace GetTrace(int depth = DefaultDepth) {
            string trace = Environment.StackTrace
                .Split(new char[1] {'\n'}, depth, StringSplitOptions.RemoveEmptyEntries)
                .Skip(SkipDepth)
                .First();

            trace = trace.TrimEnd('\r');
            trace = trace.Substring(trace.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            trace = trace.Replace(".cs:line ", ":");

            string[] traceSplit = trace.Split(':');

            return new LogTrace {
                Name = traceSplit[0],
                Line = traceSplit[1],
            };
        }
    }
}