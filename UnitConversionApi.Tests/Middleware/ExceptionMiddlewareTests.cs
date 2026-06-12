using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using UnitConversionApi.Middleware;
using UnitConversionApi.Models;

namespace UnitConversionApi.Tests.Middleware;

/// <summary>
/// Unit tests for ExceptionMiddleware.
/// </summary>
public class ExceptionMiddlewareTests
{
    /// <summary>
    /// Verify that ArgumentException returns
    /// HTTP 400 Bad Request.
    /// </summary>
    [Fact]
    public async Task InvokeAsync_ArgumentException_ReturnsBadRequest()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();

        var responseBody = new MemoryStream();
        httpContext.Response.Body = responseBody;

        RequestDelegate next =
            (HttpContext context) =>
            {
                throw new ArgumentException(
                    "Invalid unit.");
            };

        var logger =
            Mock.Of<ILogger<ExceptionMiddleware>>();

        var middleware =
            new ExceptionMiddleware(
                next,
                logger);

        // Act
        await middleware.InvokeAsync(
            httpContext);

        // Assert
        Assert.Equal(
            StatusCodes.Status400BadRequest,
            httpContext.Response.StatusCode);

        responseBody.Seek(0, SeekOrigin.Begin);

        var responseText =
            await new StreamReader(responseBody)
                .ReadToEndAsync();

        var errorResponse =
            JsonSerializer.Deserialize<ErrorResponse>(
                responseText,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        Assert.NotNull(errorResponse);

        Assert.Equal(
            400,
            errorResponse.StatusCode);

        Assert.Equal(
            "Invalid unit.",
            errorResponse.Message);
    }

    /// <summary>
    /// Verify that unexpected exceptions return
    /// HTTP 500 Internal Server Error.
    /// </summary>
    [Fact]
    public async Task InvokeAsync_UnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();

        var responseBody = new MemoryStream();
        httpContext.Response.Body = responseBody;

        RequestDelegate next =
            (HttpContext context) =>
            {
                throw new Exception(
                    "Unexpected failure.");
            };

        var logger =
            Mock.Of<ILogger<ExceptionMiddleware>>();

        var middleware =
            new ExceptionMiddleware(
                next,
                logger);

        // Act
        await middleware.InvokeAsync(
            httpContext);

        // Assert
        Assert.Equal(
            StatusCodes.Status500InternalServerError,
            httpContext.Response.StatusCode);

        responseBody.Seek(0, SeekOrigin.Begin);

        var responseText =
            await new StreamReader(responseBody)
                .ReadToEndAsync();

        var errorResponse =
            JsonSerializer.Deserialize<ErrorResponse>(
                responseText,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        Assert.NotNull(errorResponse);

        Assert.Equal(
            500,
            errorResponse.StatusCode);
    }

    /// <summary>
    /// Verify middleware allows request
    /// to continue when no exception occurs.
    /// </summary>
    [Fact]
    public async Task InvokeAsync_NoException_ContinuesPipeline()
    {
        // Arrange
        var httpContext =
            new DefaultHttpContext();

        bool nextMiddlewareExecuted = false;

        RequestDelegate next =
            (HttpContext context) =>
            {
                nextMiddlewareExecuted = true;
                return Task.CompletedTask;
            };

        var logger =
            Mock.Of<ILogger<ExceptionMiddleware>>();

        var middleware =
            new ExceptionMiddleware(
                next,
                logger);

        // Act
        await middleware.InvokeAsync(
            httpContext);

        // Assert
        Assert.True(
            nextMiddlewareExecuted);
    }
}