using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.DTO.MIddleWare;
using System.Text.Json;

namespace Services.MiddelWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = _env.IsDevelopment() ? new ApiExceptions(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) : new ApiExceptions(context.Response.StatusCode, "Internal Server Error!!");

                //var exception = new TResponse
                //{
                //    ResponseCode = context.Response.StatusCode,
                //    ResponseMessage = ex.Message,
                //    ResponsePacket = response,
                //    ResponseStatus = false
                //};

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
