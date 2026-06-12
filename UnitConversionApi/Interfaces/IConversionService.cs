using UnitConversionApi.Models;

namespace UnitConversionApi.Interfaces;

/// <summary>
/// Defines contract for unit conversion operations.
/// </summary>
public interface IConversionService
{
    /// <summary>
    /// Converts a value from one unit to another.
    /// </summary>
    /// <param name="request">Conversion request.</param>
    /// <returns>Converted result.</returns>
    ConversionResponse Convert(ConversionRequest request);
}