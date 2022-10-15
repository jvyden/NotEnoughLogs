using System;

namespace NotEnoughLogs {
    public abstract class Logger<TArea> where TArea : Enum {
        internal abstract void Log(TArea area, Level level, string message);
    }
}