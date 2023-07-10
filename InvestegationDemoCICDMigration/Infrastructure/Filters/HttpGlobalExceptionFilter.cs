using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using DevOpsWebApp.Infrastructure.ActionResults;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using DevOpsWebApp.Logging;
using DevOpsWebApp.Models;
using DevOpsWebApp.Exceptions;

namespace DevOpsWebApp.Infrastructure.Filters
{

    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ICustomLogger logger;
        private readonly RequestInfo _requestInfo;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ICustomLogger logger, RequestInfo requestInfo)
        {
            this.env = env;
            this.logger = logger;
            this._requestInfo = requestInfo;
        }

        public void OnException(ExceptionContext context)
        {
            logger.Error(new EventId(context.Exception.HResult),
                 context.Exception
                 , context.Exception.ToString());

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            if (context.Exception.GetType() == typeof(BusinessException))
            {
                var exception = context.Exception as BusinessException;
                if (exception != null)
                {
                    var apiResult = new ApiResult<object>()
                    {
                        Status = Result.Fail,
                        Message = new ApiMessage() { Message = exception.Message, Code = exception.Code, Level = exception.Level },

                    };

                    context.Result = new BadRequestObjectResult(apiResult);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            else if (context.Exception.GetType() == typeof(ResourceNotFoundException))
            {
                var exception = context.Exception as ResourceNotFoundException;
                if (exception != null)
                {
                    var apiResult = new ApiResult<object>()
                    {
                        Status = Result.Fail,
                        Message = new ApiMessage() { Message = exception.Message, Code = exception.Code, Level = exception.Level },

                    };

                    context.Result = new NotFoundObjectResult(apiResult);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }
            }
            else if (context.Exception.GetType() == typeof(BadRequestException))
            {
                var exception = context.Exception as BadRequestException;
                if (exception != null)
                {
                    var apiResult = new ApiResult<object>()
                    {
                        Status = Result.Fail,
                        Message = new ApiMessage()
                        {
                            Message = "ModelValidationError",
                            Code = exception.Code,
                            Level = exception.Level
                        },
                    };

                    context.Result = new BadRequestObjectResult(apiResult);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            else
            {
                var apiResult = new ApiResult<object>()
                {
                    Status = Result.ServerError,
                    Message = new ApiMessage() { Message = $"SystemDown", Code = CommonHelpersMessageCode.GeneralError, Level = ApiMessageType.Error },
                };

                context.Result = new InternalServerErrorObjectResult(apiResult);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.ExceptionHandled = true;
        }
    }

}
