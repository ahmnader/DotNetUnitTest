
using DevOpsWebApp.Exceptions;
using DevOpsWebApp.Logging;
using DevOpsWebApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace DevOpsWebApp.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ICustomLogger logger;
        private readonly RequestInfo _requestInfo;

        public ErrorHandlingMiddleware(ICustomLogger logger, RequestInfo requestInfo)
        {
            this.logger = logger;
            this._requestInfo = requestInfo;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            logger.Error(new EventId(exception.HResult),
                 exception
                 , exception.ToString());
            if (exception.GetType() == typeof(BusinessException))
            {
                var businessException = exception as BusinessException;
                if (businessException != null)
                {

                    var serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var apiResult = JsonConvert.SerializeObject(new ApiResult<object>()
                    {
                        Status = Result.Fail,
                        Message = new ApiMessage() { Message = businessException.Message, Code = businessException.Code, Level = businessException.Level },

                    }, serializerSettings);

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    await context.Response.WriteAsync(apiResult);
                }
            }
            else if (exception.GetType() == typeof(ResourceNotFoundException))
            {
                var businessException = exception as ResourceNotFoundException;
                if (businessException != null)
                {

                    var serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var apiResult = JsonConvert.SerializeObject(new ApiResult<object>()
                    {
                        Status = Result.Fail,
                        Message = new ApiMessage() { Message = businessException.Message, Code = businessException.Code, Level = businessException.Level },

                    }, serializerSettings);

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                    await context.Response.WriteAsync(apiResult);
                }
            }
            else if (exception.GetType() == typeof(BadRequestException))
            {
                var badRequestException = exception as BadRequestException;
                if (badRequestException != null)
                {
                    var serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var apiResult = JsonConvert.SerializeObject(new ApiResult<object>()
                    {
                        Status = Result.Fail,
                        Message = new ApiMessage()
                        {
                            Code = badRequestException.Code,
                            Level = badRequestException.Level
                        },
                    }, serializerSettings);

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    await context.Response.WriteAsync(apiResult);
                }
            }
            else
            {
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                var apiResult = JsonConvert.SerializeObject(new ApiResult<object>()
                {
                    Status = Result.ServerError,
                    Message = new ApiMessage() { Message = $"SystemDown", Code = CommonHelpersMessageCode.GeneralError, Level = ApiMessageType.Error },
                }, serializerSettings);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(apiResult);
            }
        }
    }
}
