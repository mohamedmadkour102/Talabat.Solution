using System.Net;
using System.Text.Json;
using Talabat.Errors;

namespace Talabat.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger , IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env; 
        }
        public async Task InvokeAsync(HttpContext httpContext )
        {
            try
            {
                // hmsk eh request ab3to llnext middleware lw 7sal excep ro7 ll catch 
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); // Development
                
                //  header handling 
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                // Body Handling 
                // in dev env make thing else make another  
                var response = _env.IsDevelopment() ?
                              new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                            : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                // change to camel case 
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                // change the response to json bec writeasync take json 
                var json = JsonSerializer.Serialize(response , options);
                await httpContext.Response.WriteAsync(json);

            }

        }
    }
}
