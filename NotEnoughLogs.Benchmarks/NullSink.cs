using NotEnoughLogs.Sinks;

namespace NotEnoughLogs.Benchmarks;

public class NullSink : ILoggerSink
{
    public void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> content)
    {
        _ = category.ToString();
        _ = content.ToString();
    }

    public void Log(LogLevel level, ReadOnlySpan<char> category, ReadOnlySpan<char> format, params object[] args)
    {
        _ = category.ToString();
        _ = format.ToString();
    }
}