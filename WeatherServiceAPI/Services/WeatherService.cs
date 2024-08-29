using Newtonsoft.Json;
using WeatherServiceAPI.Data;

namespace WeatherServiceAPI.Services;
public class WeatherService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiKey;
    private readonly WeatherContext _context;

    public WeatherService(IHttpClientFactory httpClientFactory, IConfiguration configuration, WeatherContext context)
    {
        _httpClientFactory = httpClientFactory;
        _apiKey = configuration["WeatherApi:ApiKey"];
        _context = context;
    }

    public virtual async Task<WeatherData> GetWeatherData(string location)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={location}&appid={_apiKey}");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var weatherData = JsonConvert.DeserializeObject<WeatherData>(content);

        // Save the weather data to the database
        _context.WeatherData.Add(weatherData);
        await _context.SaveChangesAsync();

        return weatherData;
    }
}


