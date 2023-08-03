using OpenAI.App.Helpers;
using System.Net;
using System.Text.Json;

namespace OpenAI.App.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var url = httpContext.GetUrlFromContext();
                var headers = httpContext.GetHeadersFromContext();
                var bodyValue = string.Empty;

                try
                {
                    bodyValue = httpContext.GetBodyFromContext();
                }
                catch (Exception e)
                {

                    _logger.LogError(e, $"Read request body of request");
                }

                _logger.LogError(ex, $"Request error \n{url} \nHeaders \n{headers} \nBody \n{bodyValue}");

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var responseJson = JsonSerializer.Serialize(new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = $"Internal Server Error. Contact administrator if you need more details"
            });

            return context.Response.WriteAsync(responseJson);
        }
    }
}
