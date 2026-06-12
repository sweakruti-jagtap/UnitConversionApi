using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitConversionApi.Controllers;
using UnitConversionApi.Interfaces;
using UnitConversionApi.Models;

namespace UnitConversionApi.Tests.Controllers;

/// <summary>
/// Unit tests for ConversionController.
/// </summary>
public class ConversionControllerTests
{
    private readonly Mock<IConversionService> _mockConversionService;
    private readonly ConversionController _conversionController;

    /// <summary>
    /// Initialize test dependencies.
    /// </summary>
    public ConversionControllerTests()
    {
        _mockConversionService =
            new Mock<IConversionService>();

        _conversionController =
            new ConversionController(
                _mockConversionService.Object);
    }

    /// <summary>
    /// Verify controller returns Ok result
    /// when conversion succeeds.
    /// </summary>
    [Fact]
    public void Convert_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var conversionRequest =
            new ConversionRequest
            {
                Value = 10,
                FromUnit = "meter",
                ToUnit = "foot"
            };

        var conversionResponse =
            new ConversionResponse
            {
                OriginalValue = 10,
                FromUnit = "meter",
                ToUnit = "foot",
                ConvertedValue = 32.8084
            };

        _mockConversionService
            .Setup(service =>
                service.Convert(conversionRequest))
            .Returns(conversionResponse);

        // Act
        var actionResult =
            _conversionController.Convert(
                conversionRequest);

        // Assert
        var okResult =
            Assert.IsType<OkObjectResult>(
                actionResult);

        Assert.Equal(
            200,
            okResult.StatusCode ?? 200);
    }

    /// <summary>
    /// Verify controller returns
    /// expected response payload.
    /// </summary>
    [Fact]
    public void Convert_ValidRequest_ReturnsExpectedResponse()
    {
        // Arrange
        var conversionRequest =
            new ConversionRequest
            {
                Value = 5,
                FromUnit = "kilogram",
                ToUnit = "pound"
            };

        var expectedResponse =
            new ConversionResponse
            {
                OriginalValue = 5,
                FromUnit = "kilogram",
                ToUnit = "pound",
                ConvertedValue = 11.0231
            };

        _mockConversionService
            .Setup(service =>
                service.Convert(conversionRequest))
            .Returns(expectedResponse);

        // Act
        var actionResult =
            _conversionController.Convert(
                conversionRequest);

        var okResult =
            Assert.IsType<OkObjectResult>(
                actionResult);

        var response =
            Assert.IsType<ConversionResponse>(
                okResult.Value);

        // Assert
        Assert.Equal(
            expectedResponse.ConvertedValue,
            response.ConvertedValue);
    }

    /// <summary>
    /// Verify controller calls service once.
    /// </summary>
    [Fact]
    public void Convert_ValidRequest_CallsServiceOnce()
    {
        // Arrange
        var conversionRequest =
            new ConversionRequest
            {
                Value = 100,
                FromUnit = "celsius",
                ToUnit = "fahrenheit"
            };

        _mockConversionService
            .Setup(service =>
                service.Convert(conversionRequest))
            .Returns(new ConversionResponse());

        // Act
        _conversionController.Convert(
            conversionRequest);

        // Assert
        _mockConversionService.Verify(
            service =>
                service.Convert(conversionRequest),
            Times.Once);
    }
}