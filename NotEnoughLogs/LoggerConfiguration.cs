using NotEnoughLogs.Behaviour;

namespace NotEnoughLogs;

public class LoggerConfiguration
{
    public static readonly LoggerConfiguration Default = new();

    public LoggingBehaviour Behaviour { get; set; } = new DirectLoggingBehaviour();
    public LogLevel MaxLevel { get; set; } = LogLevel.Info;
}