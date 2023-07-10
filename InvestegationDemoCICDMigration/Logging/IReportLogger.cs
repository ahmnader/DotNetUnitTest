
namespace DevOpsWebApp.Logging
{
    public interface IReportLogger
    {
        void Log(string msg);
        Task<string> SaveToFileAsync();
    }
}
