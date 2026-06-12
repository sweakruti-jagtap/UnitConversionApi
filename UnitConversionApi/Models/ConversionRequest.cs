using System.ComponentModel.DataAnnotations;

namespace UnitConversionApi.Models;

/// <summary>
/// Represents the incoming request for unit conversion.
/// </summary>
public class ConversionRequest
{
    /// <summary>
    /// Numeric value to be converted.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Source unit.
    /// Example: meter, kilogram, celsius
    /// </summary>
    [Required]
    public string FromUnit { get; set; } = string.Empty;

    /// <summary>
    /// Target unit.
    /// Example: foot, pound, fahrenheit
    /// </summary>
    [Required] 
    public string ToUnit { get; set; } = string.Empty;
}