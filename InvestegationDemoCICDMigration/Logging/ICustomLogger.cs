using DevOpsWebApp.Models;
using System.Runtime.CompilerServices;

namespace DevOpsWebApp.Logging
{
    public interface ICustomLogger
    {
        void Info(string msg, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
        void Info(LogInfo logInfo, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
        void Error(string msg, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
        void Error(LogInfo logInfo, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
        void Error(Exception ex, string messsage = "", [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
        void Error(EventId eventId, Exception ex, string messsage = "", [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
        void Debug(string msg, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
        void Debug(LogInfo logInfo, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
        void Critical(string msg, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
        void Warn(string msg, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
        void Trace(string msg, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
        void Trace(LogInfo logInfo, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
    }

}
