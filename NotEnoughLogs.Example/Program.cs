using NotEnoughLogs;
using NotEnoughLogs.Example;
using NotEnoughLogs.Loggers;

// It is recommended to store this statically somewhere accessible by your entire application.
using var logger = new LoggerContainer<ExampleArea>();
logger.RegisterLogger(new ConsoleLogger());

logger.LogInfo(ExampleArea.Startup, "Welcome to NotEnoughLogs!");
logger.LogWarning(ExampleArea.ErrorHandling, "Something may have gone wrong...");
logger.LogError(ExampleArea.Math, "Tried to divide by 0");
logger.LogCritical(ExampleArea.ErrorHandling, "Assertion error!");
logger.LogDebug(ExampleArea.Math, "Doing 2+2...");
logger.LogTrace(ExampleArea.Logger, "Trace deez nuts");