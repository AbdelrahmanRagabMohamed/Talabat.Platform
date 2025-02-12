using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate Next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = Next;
        _logger = logger;
        _env = env;
    }

    // InvokeAsync

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            // Production => LOG ex in Database (optional)

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;    // 500

            ///if (_env.IsDevelopment())
            ///{
            ///    var Respone = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());
            ///}
            ///
            ///else
            ///{
            ///    var Respone = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
            ///}

            var Respone = _env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                                               : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

            // Convert PascalCase To CamelCase Because JS can understand CamelCase only 
            var Options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Convert form objet to string => [Serialize]
            var JsonRespone = JsonSerializer.Serialize(Respone, Options);
            await context.Response.WriteAsync(JsonRespone);

        }
    }
}
