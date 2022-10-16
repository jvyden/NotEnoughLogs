using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NotEnoughLogs.Definitions;

namespace NotEnoughLogs {
    public class LoggerContainer<TContext> : IDisposable where TContext : Enum {
        private readonly List<LoggerBase> loggers = new List<LoggerBase>();
        private readonly ConcurrentQueue<LogLine> logQueue = new ConcurrentQueue<LogLine>();
        private const int logQueueDelayMs = 20;

        private readonly Task logQueueTask;
        private bool stopSignal;
        private readonly int extraTraceLines;
            
        public LoggerContainer(int extraTraceLines = 0) {
            logQueueTask = Task.Factory.StartNew(() => {
                while(true) {
                    if(!this.logQueue.TryDequeue(out LogLine line)) {
                        if(this.stopSignal) break;
                        
                        Thread.Sleep(logQueueDelayMs);
                        continue;
                    }

                    foreach(LoggerBase logger in this.loggers) {
                        logger.Log(line);
                    }
                }
            });

            this.extraTraceLines = extraTraceLines;
        }

        internal void Log(LogLine line) {
            this.logQueue.Enqueue(line);
        }

        private void log(Enum context, Level level, string message) {
            this.Log(new LogLine {
                Level = level,
                Context = context,
                Message = message,
                Trace = TraceHelper.GetTrace(extraTraceLines: this.extraTraceLines),
            });
        }

        public void RegisterLogger(LoggerBase logger) {
            this.loggers.Add(logger);
        }

        public void LogCritical(TContext context, string message) => this.log(context, Level.Critical, message);
        public void LogError(TContext context, string message) => this.log(context, Level.Error, message);
        public void LogWarning(TContext context, string message) => this.log(context, Level.Warning, message);
        public void LogInfo(TContext context, string message) => this.log(context, Level.Info, message);
        public void LogDebug(TContext context, string message) => this.log(context, Level.Debug, message);
        public void LogTrace(TContext context, string message) => this.log(context, Level.Trace, message);
        
        public void Dispose() {
            this.stopSignal = true;
            this.logQueueTask.Wait();
            this.logQueueTask.Dispose();
        }
    }
}