using System;
using System.Globalization;
using System.Text;

namespace DevOpsWebApp.Models
{
    public class LogInfo
    {
        public string? SessionID { get; set; }
        public string? RqUID { get; set; }
        public string? Username { get; set; }
        public string? RequestContent { get; set; }
        public string? ResponseModel { get; set; }
        public string? Uri { get; set; }
        public object? Headers { get; set; }
        public Exception? ExceptionMessage { get; set; }
        public long? EllapsedTime { get; set; }

        public override string ToString()
        {
            StringBuilder Info = new StringBuilder();
            Info.Append("SessionID: ");
            Info.Append(SessionID);
            Info.AppendLine();

            Info.Append("RqUID: ");
            Info.Append(RqUID);
            Info.AppendLine();

            Info.Append("Username: ");
            Info.Append(Username);
            Info.AppendLine();

            Info.Append("[REQUEST] - ");
            Info.Append(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            Info.AppendLine();
            if (Headers != null)
            {
                Info.Append("[REQUEST Headers]");
                Info.AppendLine();
                Info.Append(Headers.ToString());
                Info.AppendLine();
            }
            if (RequestContent != null)
            {
                Info.Append("[REQUEST Content]");
                Info.AppendLine();
                Info.Append(RequestContent);
                Info.AppendLine();
            }
            if (Uri != null)
            {
                Info.Append("[REQUEST URL]");
                Info.AppendLine();
                Info.Append(Uri);
                Info.AppendLine();
            }

            Info.AppendLine();
            Info.Append("[RESPONSE] - ");
            Info.Append(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            if (ResponseModel != null)
            {
                Info.AppendLine();
                Info.Append(ResponseModel);

            }
            Info.AppendLine();

            if (ExceptionMessage != null)
            {
                Info.AppendLine();
                Info.Append("[EXCEPTION] - ");
                Info.Append(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                Info.AppendLine();
                Info.Append(ExceptionMessage.ToString());
            }

            Info.AppendLine(); 
            Info.Append("Elapsed time (ms): " + EllapsedTime);
             Info.AppendLine();


            return Info.ToString();
        }
    }
}
