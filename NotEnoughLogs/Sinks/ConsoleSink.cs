using System;
using System.Text;

namespace NotEnoughLogs.Sinks;

public class ConsoleSink : ILoggerSink
{
    public void Log(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> content)
    {
        WriteBracketed(Encoding.UTF8.GetBytes(level.ToString()));
        Console.Write(' ');
        WriteBracketed(category);
        Console.Write(' ');
        
        WriteByteArray(content);
        Console.WriteLine();
    }

    public void Log(LogLevel level, ReadOnlySpan<byte> category, ReadOnlySpan<byte> format, params object[] args)
    {
        this.Log(level, category, Encoding.UTF8.GetBytes(string.Format(format.ToString(), args)));
    }

    private static void WriteBracketed(ReadOnlySpan<byte> span)
    {
        Console.Write('[');
        WriteByteArray(span);
        Console.Write(']');
    }

    private static void WriteByteArray(ReadOnlySpan<byte> span)
    {
        foreach (byte b in span)
        {
            Console.Out.Write((char)b);
        }
    }
}