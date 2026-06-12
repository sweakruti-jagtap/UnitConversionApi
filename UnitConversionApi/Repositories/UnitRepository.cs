namespace UnitConversionApi.Repositories;

/// <summary>
/// Stores supported units and conversion factors.
/// Base units:
/// Length -> Meter
/// Weight -> Kilogram
/// Area -> Square Meter
/// Volume -> Liter
/// Time -> Second
/// </summary>
public static class UnitRepository
{
    public static readonly Dictionary<string, double> LengthUnits =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "meter", 1 },
            { "kilometer", 1000 },
            { "centimeter", 0.01 },
            { "millimeter", 0.001 },
            { "foot", 0.3048 },
            { "yard", 0.9144 },
            { "mile", 1609.34 },
            { "inch", 0.0254 }
        };

    public static readonly Dictionary<string, double> WeightUnits =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "kilogram", 1 },
            { "gram", 0.001 },
            { "milligram", 0.000001 },
            { "pound", 0.453592 },
            { "ounce", 0.0283495 }
        };

    public static readonly Dictionary<string, double> AreaUnits =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "squaremeter", 1 },
            { "squarekilometer", 1000000 },
            { "squarefoot", 0.092903 },
            { "acre", 4046.86 },
            { "hectare", 10000 }
        };

    public static readonly Dictionary<string, double> VolumeUnits =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "liter", 1 },
            { "milliliter", 0.001 },
            { "cubicmeter", 1000 },
            { "gallon", 3.78541 }
        };

    public static readonly Dictionary<string, double> TimeUnits =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "second", 1 },
            { "minute", 60 },
            { "hour", 3600 },
            { "day", 86400 }
        };
}