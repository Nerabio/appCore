using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using log4net;
using log4net.Config;
using log4net.Core;
using System.Reflection;

namespace Logging
{
    class Logger : ILogger
    {
        #region Properties

        private ILog _logger;

        private bool _disposed;

        #endregion

        #region Ctors

        public Logger() : this(typeof(Logger)) { }

        public Logger(Type type)
        {
            _logger = LogManager.GetLogger(type);
            Initialize("log4net.config");
        }

        public void Initialize(string configFile)
        {
            var fileInfo = new FileInfo(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, configFile));
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.ConfigureAndWatch(logRepository, fileInfo);
        }

        #endregion

        #region Is enabled

        public bool IsDebugEnabled => _logger.IsDebugEnabled;

        public bool IsInfoEnabled => _logger.IsInfoEnabled;

        public bool IsWarnEnabled => _logger.IsWarnEnabled;

        public bool IsErrorEnabled => _logger.IsErrorEnabled;

        public bool IsFatalEnabled => _logger.IsFatalEnabled;

        #endregion

        #region Log inner

        private void Log(bool isEnabled, Action<object, Exception> logAction, object message, Exception exception = null)
        {
            if (isEnabled)
                logAction(message, exception);
        }

        private void Log(bool isEnabled, Level level, object message, Exception ex = null)
        {
            if (isEnabled)
                _logger.Logger.Log(typeof(Logger), level, message, ex);
        }

        #endregion

        #region Log with message

        public void Debug(object message)
        {
            Log(IsDebugEnabled, Level.Debug, message);
        }

        public void Info(object message)
        {
            Log(IsInfoEnabled, Level.Info, message);
        }

        public void Warn(object message)
        {
            Log(IsWarnEnabled, Level.Warn, message);
        }

        public void Error(object message)
        {
            Log(IsErrorEnabled, Level.Error, message);
        }

        public void Fatal(object message)
        {
            Log(IsFatalEnabled, Level.Fatal, message);
        }

        #endregion

        #region Log with message and exception

        public void Debug(object message, Exception exception)
        {
            Log(IsDebugEnabled, Level.Debug, message, exception);
        }

        public void Info(object message, Exception exception)
        {
            Log(IsInfoEnabled, Level.Info, message, exception);
        }

        public void Warn(object message, Exception exception)
        {
            Log(IsWarnEnabled, Level.Warn, message, exception);
        }

        public void Error(object message, Exception exception)
        {
            Log(IsErrorEnabled, Level.Error, message, exception);
        }

        public void Fatal(object message, Exception exception)
        {
            Log(IsFatalEnabled, Level.Fatal, message, exception);
        }

        #endregion

        #region Log by format

        public void DebugFormat(string format, params object[] args)
        {
            Debug(string.Format(format, args));
        }

        public void InfoFormat(string format, params object[] args)
        {
            Info(string.Format(format, args));
        }

        public void WarnFormat(string format, params object[] args)
        {
            Warn(string.Format(format, args));
        }

        public void ErrorFormat(string format, params object[] args)
        {
            Error(string.Format(format, args));
        }

        public void FatalFormat(string format, params object[] args)
        {
            Fatal(string.Format(format, args));
        }

        #endregion

        #region Implement IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
                _logger = null;

            _disposed = true;
        }

        #endregion    
    }
}
