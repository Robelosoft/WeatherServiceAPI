using Microsoft.AspNetCore.Mvc;
using WeatherServiceAPI.Services;

namespace WeatherServiceAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _weatherService;

    public WeatherController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("{location}")]
    public async Task<IActionResult> Get(string location)
    {
        var weatherData = await _weatherService.GetWeatherData(location);
        return Ok(weatherData);
    }
}

