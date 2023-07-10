using DevOpsWebApp.Models;
using System.Globalization;
using DevOpsWebApp.Utils;

namespace DevOpsWebApp.Logging
{
    public class ReportLogger : IReportLogger
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        private readonly ICustomLogger _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public ReportLogger(IConfiguration config, IWebHostEnvironment env, ICustomLogger logger, IHttpContextAccessor contextAccessor)
        {
            _config = config;
            _env = env;
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public void Log(string msg)
        {
            try
            {
                _config.GetValue<int>("SessionLogCount");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public async Task<string> SaveToFileAsync()
        {
            return await Task.Run(() =>
            {
                FixedSizedQueue<string> queue = _contextAccessor.HttpContext!.Session.Get<FixedSizedQueue<string>>("SessionLog");

                if (queue == default) return string.Empty;
                string filename = "Report_" + DateTime.Now.ToString("ddMMyyyyHHmmssf", CultureInfo.InvariantCulture) + ".txt";
                string SessionLogPath = Path.Combine(_env.ContentRootPath, "Logs\\BugReports");
                if (!Directory.Exists(SessionLogPath))
                    Directory.CreateDirectory(SessionLogPath);

                string fullPath = Path.Combine(SessionLogPath, filename);
                using (StreamWriter writer = new(fullPath, false))
                {
                    foreach (var item in queue.Q)
                    {
                        writer.Write(item);
                        writer.WriteLine("====================================================");
                    }
                }
                return filename;
            });
        }
    }
}
