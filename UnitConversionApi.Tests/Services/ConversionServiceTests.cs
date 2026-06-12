using UnitConversionApi.Models;
using UnitConversionApi.Services;

namespace UnitConversionApi.Tests.Services;

/// <summary>
/// Unit tests for ConversionService.
/// </summary>
public class ConversionServiceTests
{
    private readonly ConversionService _conversionService;

    /// <summary>
    /// Constructor executed before each test.
    /// Creates service instance.
    /// </summary>
    public ConversionServiceTests()
    {
        _conversionService = new ConversionService();
    }

    /// <summary>
    /// Verify meter to foot conversion.
    /// </summary>
    [Fact]
    public void Convert_Length_MeterToFoot_ReturnsExpectedValue()
    {
        // Arrange
        var conversionRequest = new ConversionRequest
        {
            Value = 10,
            FromUnit = "meter",
            ToUnit = "foot"
        };

        // Act
        var conversionResponse =
            _conversionService.Convert(conversionRequest);

        // Assert
        Assert.Equal(
            32.8084,
            conversionResponse.ConvertedValue,
            4);
    }

    /// <summary>
    /// Verify kilogram to pound conversion.
    /// </summary>
    [Fact]
    public void Convert_Weight_KilogramToPound_ReturnsExpectedValue()
    {
        var conversionRequest = new ConversionRequest
        {
            Value = 5,
            FromUnit = "kilogram",
            ToUnit = "pound"
        };

        var conversionResponse =
            _conversionService.Convert(conversionRequest);

        Assert.Equal(
            11.0231,
            conversionResponse.ConvertedValue,
            4);
    }

    /// <summary>
    /// Verify Celsius to Fahrenheit conversion.
    /// </summary>
    [Fact]
    public void Convert_Temperature_CelsiusToFahrenheit_ReturnsExpectedValue()
    {
        var conversionRequest = new ConversionRequest
        {
            Value = 100,
            FromUnit = "celsius",
            ToUnit = "fahrenheit"
        };

        var conversionResponse =
            _conversionService.Convert(conversionRequest);

        Assert.Equal(
            212,
            conversionResponse.ConvertedValue,
            4);
    }

    /// <summary>
    /// Verify Acre to Square Meter conversion.
    /// </summary>
    [Fact]
    public void Convert_Area_AcreToSquareMeter_ReturnsExpectedValue()
    {
        var conversionRequest = new ConversionRequest
        {
            Value = 1,
            FromUnit = "acre",
            ToUnit = "squaremeter"
        };

        var conversionResponse =
            _conversionService.Convert(conversionRequest);

        Assert.Equal(
            4046.86,
            conversionResponse.ConvertedValue,
            2);
    }

    /// <summary>
    /// Verify Gallon to Liter conversion.
    /// </summary>
    [Fact]
    public void Convert_Volume_GallonToLiter_ReturnsExpectedValue()
    {
        var conversionRequest = new ConversionRequest
        {
            Value = 1,
            FromUnit = "gallon",
            ToUnit = "liter"
        };

        var conversionResponse =
            _conversionService.Convert(conversionRequest);

        Assert.Equal(
            3.7854,
            conversionResponse.ConvertedValue,
            4);
    }

    /// <summary>
    /// Verify Day to Hour conversion.
    /// </summary>
    [Fact]
    public void Convert_Time_DayToHour_ReturnsExpectedValue()
    {
        var conversionRequest = new ConversionRequest
        {
            Value = 2,
            FromUnit = "day",
            ToUnit = "hour"
        };

        var conversionResponse =
            _conversionService.Convert(conversionRequest);

        Assert.Equal(
            48,
            conversionResponse.ConvertedValue,
            4);
    }

    /// <summary>
    /// Verify exception for unsupported unit.
    /// </summary>
    [Fact]
    public void Convert_InvalidUnit_ThrowsArgumentException()
    {
        var conversionRequest = new ConversionRequest
        {
            Value = 100,
            FromUnit = "meter",
            ToUnit = "banana"
        };

        Assert.Throws<ArgumentException>(
            () => _conversionService.Convert(conversionRequest));
    }

    /// <summary>
    /// Verify exception when request is null.
    /// </summary>
    [Fact]
    public void Convert_NullRequest_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => _conversionService.Convert(null!));
    }
}