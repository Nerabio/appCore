using System;

namespace Logging
{
    public interface ILogger : IDisposable
    {
        void Initialize(string configFile);

        #region Is enabled

        bool IsDebugEnabled { get; }

        bool IsInfoEnabled { get; }

        bool IsWarnEnabled { get; }

        bool IsErrorEnabled { get; }

        bool IsFatalEnabled { get; }

        #endregion

        #region Log with message

        void Debug(object message);

        void Info(object message);

        void Warn(object message);

        void Error(object message);

        void Fatal(object message);

        #endregion

        #region Log with message and exception

        void Debug(object message, Exception exception);

        void Info(object message, Exception exception);

        void Warn(object message, Exception exception);

        void Error(object message, Exception exception);

        void Fatal(object message, Exception exception);

        #endregion

        #region Log by format

        void DebugFormat(string format, params object[] args);

        void InfoFormat(string format, params object[] args);

        void WarnFormat(string format, params object[] args);

        void ErrorFormat(string format, params object[] args);

        void FatalFormat(string format, params object[] args);

        #endregion
    }
}
