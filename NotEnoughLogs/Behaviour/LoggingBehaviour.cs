namespace NotEnoughLogs.Behaviour;

public enum LoggingBehaviour : byte
{
    /// <summary>
    /// The sinks are called directly upon calling Log.
    /// Minimal allocations, but causes multi-threading issues and blocks.
    /// </summary>
    Direct = 0,
    /// <summary>
    /// A new task for the ThreadPool is created for each log call, which then waits in a semaphore.
    /// Prevents multi-threading issues while not blocking. 
    /// </summary>
    ThreadPool = 1,
    /// <summary>
    /// The nuclear option. An entire thread is spun up to handle logs to ensure everything is in order.
    /// Higher CPU cost, completely prevents multi-threading issues while not blocking.
    /// </summary>
    Queue = 2,
}