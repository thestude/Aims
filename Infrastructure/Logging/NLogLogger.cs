using System;
using NLog;

namespace AIMS.Infrastructure.Logging
{
    public class NLogLogger : ILogger
    {
        private readonly Logger _logger;

        public NLogLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            Exception ex = x;
            if (x.InnerException != null)
                ex = x.InnerException;

            Error(LogUtility.BuildExceptionMessage(x));
            _logger.Error("Handled", ex);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            Exception ex = x;
            if (x.InnerException != null)
                ex = x.InnerException;

            Fatal(LogUtility.BuildExceptionMessage(x));
            _logger.Fatal("Handled", ex);
        }
    }
}