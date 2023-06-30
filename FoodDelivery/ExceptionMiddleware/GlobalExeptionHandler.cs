using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace FoodDelivery.ExceptionMiddleware
{

    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application;json";

                switch (ex)
                {
                    case NullReferenceException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        _logger.LogError(ex, "Referring to an object that has the value null");
                        await context.Response.WriteAsync("Referring to an object that has the value null");
                        break;

                    case UnauthorizedAccessException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        _logger.LogError(ex, "You must be logged in before contacting");
                        await context.Response.WriteAsync("You must be logged in before contacting");
                        break;

                    case ArgumentException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        _logger.LogError(ex, "Error in query arguments");
                        await context.Response.WriteAsync("Error in query arguments");
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        _logger.LogError(ex, "InternalServerError");
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = response });
                await response.WriteAsync(result);
            }

        }

    }
}
