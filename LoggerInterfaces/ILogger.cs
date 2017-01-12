using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerInterfaces
{
    /// <summary>
    /// Provides most popular methods of loggers
    /// </summary>
    public interface ILogger
    {
        void Debug(string message);
        void Debug(string message, params object[] args);
        void Debug(Exception exception, string message, params object[] args);
        void Error(string message);
        void Error(string message, params object[] args);
        void Error(Exception exception, string message, params object[] args);
        void Fatal(string message);
        void Fatal(string message, params object[] args);
        void Fatal(Exception exception, string message, params object[] args);
        void Info(string message);
        void Info(string message, params object[] args);
        void Info(Exception exception, string message, params object[] args);
        void Warn(string message);
        void Warn(string message, params object[] args);
        void Warn(Exception exception, string message, params object[] args);
        void Trace(string message);
        void Trace(string message, params object[] args);
        void Trace(Exception exception, string message, params object[] args);
    }
}
