using DevOpsWebApp.Logging;
using DevOpsWebApp.Models;
using System.Runtime.CompilerServices;

namespace DevOpsWebApp.Logging
{
    public class Logger : ICustomLogger
    {
        private readonly ILogger<Logger> _logger;
        private readonly RequestInfo _requestInfo;

        public Logger(ILogger<Logger> logger, RequestInfo requestInfo)
        {
            _logger = logger;
            this._requestInfo = requestInfo;
        }


         
        public void Info(string msg, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileName(filePath); 

            _logger.LogInformation("{msg} {ClassName} {MethodName} {LineNumber} {RqUID}", msg, className, methodName, lineNumber, _requestInfo.RqUID);
            
        }

        public void Info(LogInfo logInfo, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileName(filePath);

            _logger.LogInformation("{msg} {ClassName} {MethodName} {LineNumber} {RqUID}", logInfo.ToString(), className, methodName, lineNumber, _requestInfo.RqUID);

        }

        public void Error(string msg, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileName(filePath);
              
            _logger.LogError("{msg} {ClassName} {MethodName} {LineNumber} {RqUID}", msg, className, methodName, lineNumber, _requestInfo.RqUID);
              
        }

        
        public void Error(Exception ex, string messsage = "", [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileName(filePath); 

            _logger.LogError(ex, "{msg} {ClassName} {MethodName} {LineNumber} {RqUID}", messsage, className, methodName, lineNumber, _requestInfo.RqUID);
             
        }

        public void Error(EventId eventId, Exception ex, string messsage = "", [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileName(filePath); 

            _logger.LogError(eventId, ex, "{msg} {ClassName} {MethodName} {LineNumber} {RqUID}", messsage, className, methodName, lineNumber, _requestInfo.RqUID);
             
        }

        public void Error(LogInfo logInfo, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileName(filePath);
              
            _logger.LogError("{msg} {ClassName} {MethodName} {LineNumber} {RqUID}", logInfo.ExceptionMessage, className, methodName, lineNumber, _requestInfo.RqUID);
             
        }

        public void Debug(string msg, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileName(filePath); 

            _logger.LogDebug("{msg} {ClassName} {MethodName} {LineNumber} {RqUID}", msg, className, methodName, lineNumber, _requestInfo.RqUID);
             
        }

        public void Debug(LogInfo logInfo, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileName(filePath);
              
            _logger.LogDebug("{msg} {ClassName} {MethodName} {LineNumber} {RqUID}", logInfo.ExceptionMessage, className, methodName, lineNumber, _requestInfo.RqUID);
             
        }

        public void Critical(string msg, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileName(filePath); 

            _logger.LogCritical("{msg} {ClassName} {MethodName} {LineNumber} {RqUID}", msg, className, methodName, lineNumber, _requestInfo.RqUID);
             
        }

        public void Warn(string msg, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileName(filePath); 

            _logger.LogWarning("{msg} {ClassName} {MethodName} {LineNumber} {RqUID}", msg, className, methodName, lineNumber, _requestInfo.RqUID);
             
        }

        public void Trace(string msg, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileName(filePath); 

            _logger.LogTrace("{msg} {ClassName} {MethodName} {LineNumber} {RqUID}", msg, className, methodName, lineNumber, _requestInfo.RqUID);
             
        }

        public void Trace(LogInfo logInfo, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileName(filePath); 

            _logger.LogTrace("{msg} {ClassName} {MethodName} {LineNumber} {RqUID}", logInfo.ExceptionMessage, className, methodName, lineNumber, _requestInfo.RqUID);
             
        }
    }
}
