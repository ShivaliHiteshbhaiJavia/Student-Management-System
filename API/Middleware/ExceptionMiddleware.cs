using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode = (int)HttpStatusCode.InternalServerError;
            int customCode = 50000;
            string message = "Internal Server Error";

            // Handle custom exception
            if (exception.GetType().Name == "ResourceNotFoundException")
            {
                statusCode = (int)HttpStatusCode.NotFound; // 404
                customCode = (int)exception.GetType()
                    .GetProperty("StatusCode")
                    ?.GetValue(exception);

                message = exception.Message;
            }

            var response = new
            {
                status = statusCode,
                code = customCode,
                message = message
            };

            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}