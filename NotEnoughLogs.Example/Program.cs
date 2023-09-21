using NotEnoughLogs;
using NotEnoughLogs.Behaviour;
using NotEnoughLogs.Example;

Logger logger = new(new LoggerConfiguration
{
    Behaviour = new DirectLoggingBehaviour(),
    MaxLevel = LogLevel.Trace
});
logger.LogInfo(ExampleCategory.Startup, "Welcome to NotEnoughLogs!");
logger.LogWarning(ExampleCategory.ErrorHandling, "Something may have gone wrong...");
logger.LogError(ExampleCategory.Math, "Tried to divide by 0");
logger.LogCritical(ExampleCategory.ErrorHandling, "Assertion error!");
logger.LogDebug(ExampleCategory.Math, "Doing 2+2...");
logger.LogTrace(ExampleCategory.Logger, "Trace deez nuts");