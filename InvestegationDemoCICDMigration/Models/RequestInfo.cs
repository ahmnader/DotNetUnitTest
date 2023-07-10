namespace DevOpsWebApp.Models
{
    public class RequestInfo
    {
        public string? SessionID { get; set; }
        public string? RqUID { get; set; }
    }


    public class BackEndRequestInfo : RequestInfo
    {
        public BackEndRequestInfo(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            var context = httpContextAccessor.HttpContext;

            if (context != null)
            {
                RqUID = context.Request.Headers["X_MOF_RqUID"].FirstOrDefault();


                RqUID ??= Guid.NewGuid().ToString();
            }
        }
    }
}
