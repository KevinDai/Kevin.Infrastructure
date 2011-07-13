using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace Kevin.Infrastructure.Logging.Log4net
{

    public class Log4netLogger : ILogger
    {

        #region Members

        protected ILog Log
        {
            get
            {
                return _log;
            }
        }
        private ILog _log;

        #endregion

        public Log4netLogger(string loggerName)
        {
            XmlConfigurator.Configure();
            _log = LogManager.GetLogger(loggerName);
            if (_log == null)
            {
                throw new ArgumentException(
                    string.Format(Resources.Messages.exception_GetLoggerInvalidLoggerName, loggerName)
                    );
            }
        }

        public Log4netLogger(ILog log)
        {
            if (log == null)
            {
                throw new ArgumentNullException("log");
            }
            _log = log;
        }

        #region ILogger implementation

        public void Debug(string message)
        {
            Log.Debug(message);
        }


        public void Debug(string message, Exception ex)
        {
            Log.Debug(message, ex);
        }

        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            Log.Error(message, ex);
        }

        public void Fatal(string message)
        {
            Log.Fatal(message);
        }

        public void Fatal(string message, Exception ex)
        {
            Log.Fatal(message, ex);
        }

        public void Info(string message)
        {
            Log.Info(message);
        }

        public void Info(string message, Exception ex)
        {
            Log.Info(message, ex);
        }

        public void Warn(string message)
        {
            Log.Warn(message);
        }

        public void Warn(string message, Exception ex)
        {
            Log.Warn(message, ex);
        }

        #endregion
    }
}
