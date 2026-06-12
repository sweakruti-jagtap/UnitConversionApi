using UnitConversionApi.Interfaces;
using UnitConversionApi.Middleware;
using UnitConversionApi.Services;

var builder = WebApplication.CreateBuilder(args);

#region Service Registration

// Register MVC Controllers
builder.Services.AddControllers();

// Register API Explorer required for Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();

// Register Swagger generator for API documentation
builder.Services.AddSwaggerGen();

// Register application services for Dependency Injection
builder.Services.AddScoped<IConversionService, ConversionService>();

#endregion

var app = builder.Build();

#region Middleware Pipeline

// Global exception handling middleware
// Captures unhandled exceptions and returns standardized error responses
app.UseMiddleware<ExceptionMiddleware>();

// Enable Swagger UI only in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Unit Conversion API";
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "Unit Conversion API V1");
    });
}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Enable authorization middleware
// Included for future scalability even though authentication
// is not currently implemented.
app.UseAuthorization();

#endregion

#region Endpoint Mapping

// Map controller endpoints
app.MapControllers();

// Health check endpoint
// Useful for monitoring and deployment validation
app.MapGet("/health", () => Results.Ok(new
{
    Status = "Healthy",
    Timestamp = DateTime.UtcNow
}));

#endregion

// Start the application
app.Run();