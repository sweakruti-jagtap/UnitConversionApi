namespace UnitConversionApi.Models;

/// <summary>
/// Standard error response returned by API.
/// </summary>
public class ErrorResponse
{
    public int StatusCode { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? Details { get; set; }
}