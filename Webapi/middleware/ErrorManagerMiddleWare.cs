

using System.Net;
using Application.errorHandlers;
using Newtonsoft.Json;

namespace Persistence.middleware
{
    public class ErrorManagerMiddleWare
    {
        private readonly RequestDelegate? _next;
        private readonly ILogger<ErrorManagerMiddleWare>? _logger;
        public ErrorManagerMiddleWare(RequestDelegate next, ILogger<ErrorManagerMiddleWare> logger){
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context){
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await ExceptionHandlerAsync(context, e, _logger);
            }
        }

        private async Task ExceptionHandlerAsync(HttpContext context, Exception e, ILogger<ErrorManagerMiddleWare> logger){
            object errors = null;
            switch (e)
            {
                case ExceptionHandler eh:
                    logger.LogError(e,"error handler");
                    errors = eh.Errors;
                    context.Response.StatusCode = (int)eh.StatusCode;
                    break;
                case Exception ex:
                    logger.LogError(e,"Server error");
                    errors = string.IsNullOrWhiteSpace (ex.Message) ? "Error" : ex.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
                
            }
            context.Response.ContentType = "application/json";
            if (errors != null){
                var results = JsonConvert.SerializeObject(new {errors});
                await context.Response.WriteAsync(results);
            }
        }
    }
}