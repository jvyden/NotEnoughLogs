// using NotEnoughLogs;
// using NotEnoughLogs.Example;
// using NotEnoughLogs.Loggers;
//
// // It is recommended to store this statically somewhere accessible by your entire application.
// using LoggerContainer<ExampleContext> logger = new LoggerContainer<ExampleContext>();
// logger.RegisterLogger(new ConsoleLogger());
//
// logger.LogInfo(ExampleContext.Startup, "Welcome to NotEnoughLogs!");
// logger.LogWarning(ExampleContext.ErrorHandling, "Something may have gone wrong...");
// logger.LogError(ExampleContext.Math, "Tried to divide by 0");
// logger.LogCritical(ExampleContext.ErrorHandling, "Assertion error!");
// logger.LogDebug(ExampleContext.Math, "Doing 2+2...");
// logger.LogTrace(ExampleContext.Logger, "Trace deez nuts");
//
// StaticLogger.LogInfo(ExampleContext.Logger, "Static logger test");
// StaticLogger.Dispose();

using NotEnoughLogs;
using NotEnoughLogs.Behaviour;
using NotEnoughLogs.Sinks;

Logger logger = new(new []{ new NullSink() }, new LoggerConfiguration
{
    // Behaviour = LoggingBehaviour.ThreadPool
});

for (int i = 0; i < 5_000_000; i++)
{
    logger.Log(LogLevel.Info, "Test"u8, "yo man"u8);
}