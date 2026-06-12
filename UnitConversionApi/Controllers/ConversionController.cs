using Microsoft.AspNetCore.Mvc;
using UnitConversionApi.Interfaces;
using UnitConversionApi.Models;

namespace UnitConversionApi.Controllers;

/// <summary>
/// API endpoints for unit conversion.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ConversionController : ControllerBase
{
    private readonly IConversionService _service;

    /// <summary>
    /// Constructor using Dependency Injection.
    /// </summary>
    public ConversionController(
        IConversionService service)
    {
        _service = service;
    }

    /// <summary>
    /// Converts a value between supported units.
    /// </summary>
    /// <param name="request">Conversion details.</param>
    /// <returns>Converted value.</returns>
    [HttpPost]
    public IActionResult Convert(
    ConversionRequest request)
    {
        var result =
            _service.Convert(request);

        return Ok(result);
    }
}