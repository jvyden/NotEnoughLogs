// See https://aka.ms/new-console-template for more information

using NotEnoughLogs;
using NotEnoughLogs.Example;
using NotEnoughLogs.Loggers;

var logger = new LoggerContainer<ExampleArea>();
logger.RegisterLogger(new ConsoleLogger<ExampleArea>());

logger.LogInfo(ExampleArea.Program, "Welcome to NotEnoughLogs!");