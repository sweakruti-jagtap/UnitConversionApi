using UnitConversionApi.Interfaces;
using UnitConversionApi.Models;
using UnitConversionApi.Repositories;

namespace UnitConversionApi.Services;

/// <summary>
/// Service responsible for performing unit conversions.
/// Supports Length, Weight, Area, Volume, Time and Temperature conversions.
/// </summary>
public class ConversionService : IConversionService
{
    /// <summary>
    /// Converts a value from one unit to another.
    /// </summary>
    /// <param name="request">Conversion request details.</param>
    /// <returns>Converted result.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when request is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when input values are invalid or unsupported.
    /// </exception>
    public ConversionResponse Convert(ConversionRequest request)
    {
        // Validate request object
        ArgumentNullException.ThrowIfNull(request);

        // Validate source unit
        if (string.IsNullOrWhiteSpace(request.FromUnit))
        {
            throw new ArgumentException(
                "Source unit (FromUnit) is required.");
        }

        // Validate target unit
        if (string.IsNullOrWhiteSpace(request.ToUnit))
        {
            throw new ArgumentException(
                "Target unit (ToUnit) is required.");
        }

        // Convert units to lowercase for comparison
        var fromUnit = request.FromUnit.ToLower();
        var toUnit = request.ToUnit.ToLower();

        // Temperature conversion requires custom formulas
        // and cannot use multiplication factors.
        if (IsTemperature(fromUnit, toUnit))
        {
            return ConvertTemperature(request);
        }

        // Attempt conversion across supported categories.
        var result =
            TryDictionaryConversion(request, UnitRepository.LengthUnits)
            ?? TryDictionaryConversion(request, UnitRepository.WeightUnits)
            ?? TryDictionaryConversion(request, UnitRepository.AreaUnits)
            ?? TryDictionaryConversion(request, UnitRepository.VolumeUnits)
            ?? TryDictionaryConversion(request, UnitRepository.TimeUnits);

        return result ??
               throw new ArgumentException(
                   $"Unsupported conversion from '{request.FromUnit}' to '{request.ToUnit}'.");
    }

    /// <summary>
    /// Generic conversion logic for all factor-based units.
    /// Example: Meter → Foot, Kilogram → Pound.
    /// </summary>
    /// <param name="request">Conversion request.</param>
    /// <param name="units">Dictionary containing conversion factors.</param>
    /// <returns>Conversion result if units exist; otherwise null.</returns>
    private ConversionResponse? TryDictionaryConversion(
        ConversionRequest request,
        Dictionary<string, double> units)
    {
        // Check whether both units belong to same category.
        if (!units.ContainsKey(request.FromUnit) ||
            !units.ContainsKey(request.ToUnit))
        {
            return null;
        }

        // Convert source value to base unit.
        double baseValue =
            request.Value *
            units[request.FromUnit];

        // Convert base unit to target unit.
        double convertedValue =
            baseValue /
            units[request.ToUnit];

        return BuildResponse(request, convertedValue);
    }

    /// <summary>
    /// Determines whether the supplied units belong
    /// to the temperature conversion category.
    /// </summary>
    private bool IsTemperature(
        string fromUnit,
        string toUnit)
    {
        string[] temperatureUnits =
        {
            "celsius",
            "fahrenheit",
            "kelvin"
        };

        return temperatureUnits.Contains(fromUnit) &&
               temperatureUnits.Contains(toUnit);
    }

    /// <summary>
    /// Performs temperature conversion using formulas.
    /// </summary>
    /// <param name="request">Conversion request.</param>
    /// <returns>Temperature conversion result.</returns>
    private ConversionResponse ConvertTemperature(
        ConversionRequest request)
    {
        // Convert source temperature to Celsius first.
        double celsius =
            request.FromUnit.ToLower() switch
            {
                "celsius" => request.Value,
                "fahrenheit" => (request.Value - 32) * 5 / 9,
                "kelvin" => request.Value - 273.15,
                _ => throw new ArgumentException(
                    "Invalid temperature unit.")
            };

        // Convert Celsius to target temperature unit.
        double convertedValue =
            request.ToUnit.ToLower() switch
            {
                "celsius" => celsius,
                "fahrenheit" => celsius * 9 / 5 + 32,
                "kelvin" => celsius + 273.15,
                _ => throw new ArgumentException(
                    "Invalid temperature unit.")
            };

        return BuildResponse(
            request,
            convertedValue);
    }

    /// <summary>
    /// Creates standardized API response.
    /// </summary>
    /// <param name="request">Original request.</param>
    /// <param name="convertedValue">Calculated value.</param>
    /// <returns>Formatted conversion response.</returns>
    private ConversionResponse BuildResponse(
        ConversionRequest request,
        double convertedValue)
    {
        return new ConversionResponse
        {
            OriginalValue = request.Value,
            FromUnit = request.FromUnit,
            ToUnit = request.ToUnit,
            ConvertedValue = Math.Round(convertedValue, 4)
        };
    }
}