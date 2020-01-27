using System.Collections.Generic;
using System.Net;
using Api.Infrastructure.ActionResults;
using ElmahCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Infrastructure.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
               context.Exception,
               context.Exception.Message);

            var json = new JsonErrorResponse();

            json.Messages.Add("servidor", new List<string> { "Ocorreu uma falha interna no servidor." });

            if (_env.IsDevelopment())
            {
                json.DeveloperMeesage = context.Exception;
            }

            context.Result = new InternalServerErrorObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.HttpContext.RiseError(context.Exception);

            context.ExceptionHandled = true;
        }
    }
}