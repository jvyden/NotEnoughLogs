using NotEnoughLogs;
using NotEnoughLogs.Example;
using NotEnoughLogs.Loggers;

// It is recommended to store this statically somewhere accessible by your entire application.
using LoggerContainer<ExampleContext> logger = new LoggerContainer<ExampleContext>();
logger.RegisterLogger(new ConsoleLogger());

logger.LogInfo(ExampleContext.Startup, "Welcome to NotEnoughLogs!");
logger.LogWarning(ExampleContext.ErrorHandling, "Something may have gone wrong...");
logger.LogError(ExampleContext.Math, "Tried to divide by 0");
logger.LogCritical(ExampleContext.ErrorHandling, "Assertion error!");
logger.LogDebug(ExampleContext.Math, "Doing 2+2...");
logger.LogTrace(ExampleContext.Logger, "Trace deez nuts");

StaticLogger.LogInfo(ExampleContext.Logger, "Static logger test");
StaticLogger.Dispose();