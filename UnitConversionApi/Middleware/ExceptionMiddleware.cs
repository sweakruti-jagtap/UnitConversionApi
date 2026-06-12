using System.Net;
using System.Text.Json;
using UnitConversionApi.Models;

namespace UnitConversionApi.Middleware;

/// <summary>
/// Global exception handling middleware.
/// Ensures consistent API error responses.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unhandled exception occurred.");

            await HandleExceptionAsync(
                context,
                ex);
        }
    }

    private static Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;

        if (exception is ArgumentException)
        {
            statusCode = HttpStatusCode.BadRequest;
        }

        var response = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Message = exception.Message,
            Details = exception.InnerException?.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}