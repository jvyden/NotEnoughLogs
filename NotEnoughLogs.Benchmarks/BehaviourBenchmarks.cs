using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using NotEnoughLogs.Behaviour;
using NotEnoughLogs.Sinks;

namespace NotEnoughLogs.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RunStrategy.Throughput, invocationCount: 5_000_000)]
public class BehaviourBenchmarks
{
    // [Params(LoggingBehaviour.Queue, LoggingBehaviour.Direct)]
    public LoggingBehaviour Behaviour;

    private readonly List<ILoggerSink> _sinks = new(1)
    {
        new NullSink()
    };

    private Logger _logger;

    [IterationSetup]
    public void Setup()
    {
        this._logger = new Logger(_sinks, new LoggerConfiguration
        {
            Behaviour = this.Behaviour
        });
    }
    
    [Benchmark]
    public void Logic()
    {
        this._logger.Log(LogLevel.Info, "Category", "ContentContentContentContentContentContentContentContentContentContent");
    }
}