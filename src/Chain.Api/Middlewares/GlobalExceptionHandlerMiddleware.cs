using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.Json;

namespace Chain.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = exception switch
                {
                    NullReferenceException => (int)StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status400BadRequest,
                };


                var result = System.Text.Json.JsonSerializer.Serialize(new { message = exception.Message });
                await response.WriteAsync(result);
            }
        }
    }

    public static class GlobalExcepionHandlerExtension
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
