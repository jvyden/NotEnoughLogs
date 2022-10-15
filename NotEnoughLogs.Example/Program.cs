using NotEnoughLogs;
using NotEnoughLogs.Example;
using NotEnoughLogs.Loggers;

var logger = new LoggerContainer<ExampleArea>();
logger.RegisterLogger(new ConsoleLogger());

logger.LogInfo(ExampleArea.Startup, "Welcome to NotEnoughLogs!");
logger.LogWarning(ExampleArea.ErrorHandling, "Something may have gone wrong...");
logger.LogError(ExampleArea.Math, "Tried to divide by 0");
logger.LogCritical(ExampleArea.ErrorHandling, "Assertion error!");
logger.LogDebug(ExampleArea.Math, "Doing 2+2...");
logger.LogTrace(ExampleArea.Logger, "Trace deez nuts");

var memoryLogger = new MemoryLogger();
memoryLogger.Log(ExampleArea.Logger, Level.Info, "Things logged to the memory logger will stay in memory until they are dumped.");
memoryLogger.DumpToContainer(logger);
memoryLogger.Log(ExampleArea.Logger, Level.Info, "For example, the above message is logged to the console, but this will not be logged.");

logger.Log(memoryLogger.Logs[0]);