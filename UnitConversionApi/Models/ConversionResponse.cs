namespace UnitConversionApi.Models;

/// <summary>
/// Represents the conversion result returned to the caller.
/// </summary>
public class ConversionResponse
{
    /// <summary>
    /// Original value provided by the user.
    /// </summary>
    public double OriginalValue { get; set; }

    /// <summary>
    /// Source unit.
    /// </summary>
    public string FromUnit { get; set; } = string.Empty;

    /// <summary>
    /// Target unit.
    /// </summary>
    public string ToUnit { get; set; } = string.Empty;

    /// <summary>
    /// Calculated converted value.
    /// </summary>
    public double ConvertedValue { get; set; }
}