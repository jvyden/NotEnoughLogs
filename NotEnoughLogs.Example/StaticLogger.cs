using NotEnoughLogs.Loggers;

namespace NotEnoughLogs.Example;

public static class StaticLogger
{
    private static readonly LoggerContainer<ExampleContext> Logger = new();

    static StaticLogger()
    {
        Logger.RegisterLogger(new ConsoleLogger());
    }

    public static void LogCritical(ExampleContext context, string message)
    {
        Logger.LogCritical(context, message);
    }

    public static void LogError(ExampleContext context, string message)
    {
        Logger.LogError(context, message);
    }

    public static void LogWarning(ExampleContext context, string message)
    {
        Logger.LogWarning(context, message);
    }

    public static void LogInfo(ExampleContext context, string message)
    {
        Logger.LogInfo(context, message);
    }

    public static void LogDebug(ExampleContext context, string message)
    {
        Logger.LogDebug(context, message);
    }

    public static void LogTrace(ExampleContext context, string message)
    {
        Logger.LogTrace(context, message);
    }

    public static void Dispose()
    {
        Logger.Dispose();
    }
}