using NotEnoughLogs.Loggers;

namespace NotEnoughLogs.Example {
    public static class StaticLogger {
        private static readonly LoggerContainer<ExampleContext> logger = new(1);

        static StaticLogger() {
            logger.RegisterLogger(new ConsoleLogger());
        }

        public static void LogCritical(ExampleContext context, string message) => logger.LogCritical(context, message);
        public static void LogError(ExampleContext context, string message) => logger.LogError(context, message);
        public static void LogWarning(ExampleContext context, string message) => logger.LogWarning(context, message);
        public static void LogInfo(ExampleContext context, string message) => logger.LogInfo(context, message);
        public static void LogDebug(ExampleContext context, string message) => logger.LogDebug(context, message);
        public static void LogTrace(ExampleContext context, string message) => logger.LogTrace(context, message);

        public static void Dispose() => logger.Dispose();
    }
}