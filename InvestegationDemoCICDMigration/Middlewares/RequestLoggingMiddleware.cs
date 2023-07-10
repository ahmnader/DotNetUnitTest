using DevOpsWebApp.Logging;
using System.Diagnostics;

namespace DevOpsWebApp.Middlewares
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        private const string V = "/";
        private readonly ICustomLogger _logger;

        public RequestLoggingMiddleware(ICustomLogger logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            
            var watch = System.Diagnostics.Stopwatch.StartNew();

            /// Log request
            if (context.Request.HasJsonContentType())
            {
                /// Read request body
                context.Request.EnableBuffering();
                string body = await StreamReading(context.Request.Body);
                 
                _logger.Info(string.Format("----- Receiving {0} request from Uri: {1} - Request: {2} - ", context.Request.Method, ReadUri(context), body));
            }
            else if (!string.IsNullOrWhiteSpace(context.Request.ContentType))
            {
                _logger.Info(string.Format("----- Receiving {0} request from Uri: {1} - Request Content Type: {2} - Content Length {3} ", context.Request.Method, ReadUri(context), context.Request.ContentType, context.Request.ContentLength?.ToString()));

            }
            else
            { 
                _logger.Info(string.Format("----- Receiving {0} request from Uri: {1}  ", context.Request.Method, ReadUri(context)));
            }

                
             

            await HandleResponse(context, next, watch);

             
            
        }

        private async Task HandleResponse(HttpContext context, RequestDelegate next, Stopwatch watch)
        {
             
           
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                try
                {
                    await next(context);

                    
                }
                catch (Exception ex)
                {
                    if (!context.Response.HasStarted)
                    {
                        context.Response.Body = originalBodyStream;
                    }

                    /// Log exception
                    watch.Stop();
                    _logger.Info(string.Format($"----- Response Elapsed time: {watch.ElapsedMilliseconds} ms, Error: {ex}"));


                    throw;
                }


                /// Log response
                /// 
                watch.Stop();
                
                if ( !string.IsNullOrWhiteSpace(context.Response.ContentType) && (context.Response.ContentType.StartsWith("text/plain") || context.Response.ContentType.StartsWith("application/json")))
                { 
                    _logger.Info(string.Format("----- Response Elapsed time: {0} ms, Content: {1}", watch.ElapsedMilliseconds.ToString(), await GetResponse(context.Response)));
                }
                else
                {
                    _logger.Info(string.Format("----- Response Elapsed time: {0} ms, Content Type: {1} - Content Length: {2}", watch.ElapsedMilliseconds.ToString(), context.Response.ContentType, context.Response.ContentLength.ToString()));
                }

                responseBody.Seek(0, SeekOrigin.Begin);

                await responseBody.CopyToAsync(originalBodyStream);
                
                context.Response.Body = originalBodyStream;
                
            }
            
        } 
        
        private static string ReadUri(HttpContext context)
        {
            string Uri = context.Request.Scheme + "://" + context.Request.Host + V + context.Request.Path;
            string queryString = context.Request.QueryString.ToString();
            if (!string.IsNullOrEmpty(queryString))
            {
                Uri = Uri + "?" + queryString;
            }
            return Uri;
        }
        
        private static async Task<string> GetResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await StreamReading(response.Body);
            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }
        private static async Task<string> StreamReading(Stream stream)
        {
            string text = await new StreamReader(stream)
                                                 .ReadToEndAsync();

            stream.Position = 0;
            return text;
        }




    }
}
