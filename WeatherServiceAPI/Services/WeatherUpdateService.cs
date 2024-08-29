namespace WeatherServiceAPI.Services;
public class WeatherUpdateService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly WeatherService _weatherService;

    public WeatherUpdateService(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(UpdateWeather, null, TimeSpan.Zero, TimeSpan.FromHours(1)); // Update every hour
        return Task.CompletedTask;
    }

    private void UpdateWeather(object state)
    {
        // Here you can call the WeatherService to update the weather data
        // You would need a list of locations that you want to update
        // For example:
        _weatherService.GetWeatherData("New York");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}

