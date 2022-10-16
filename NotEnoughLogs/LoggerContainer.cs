using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NotEnoughLogs.Data;

namespace NotEnoughLogs {
    public class LoggerContainer<TArea> : IDisposable where TArea : Enum {
        private readonly List<LoggerBase> loggers = new List<LoggerBase>();
        private readonly ConcurrentQueue<LogLine> logQueue = new ConcurrentQueue<LogLine>();
        private const int logQueueDelayMs = 20;

        private readonly Task logQueueTask;
        private bool stopSignal = false;
            
        public LoggerContainer() {
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
        }

        internal void Log(LogLine line) {
            this.logQueue.Enqueue(line);
        }

        private void log(Enum area, Level level, string message) {
            this.Log(new LogLine {
                Level = level,
                Area = area,
                Message = message,
                Trace = TraceHelper.GetTrace(),
            });
        }

        public void RegisterLogger(LoggerBase logger) {
            this.loggers.Add(logger);
        }

        public void LogCritical(TArea area, string message) => this.log(area, Level.Critical, message);
        public void LogError(TArea area, string message) => this.log(area, Level.Error, message);
        public void LogWarning(TArea area, string message) => this.log(area, Level.Warning, message);
        public void LogInfo(TArea area, string message) => this.log(area, Level.Info, message);
        public void LogDebug(TArea area, string message) => this.log(area, Level.Debug, message);
        public void LogTrace(TArea area, string message) => this.log(area, Level.Trace, message);
        
        public void Dispose() {
            this.stopSignal = true;
            this.logQueueTask.Wait();
            this.logQueueTask.Dispose();
        }
    }
}