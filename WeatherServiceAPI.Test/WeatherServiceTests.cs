using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using WeatherServiceAPI.Data;
using WeatherServiceAPI.Services;

namespace WeatherServiceAPI.Test;
public class WeatherServiceTests
{
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private Mock<IConfiguration> _configurationMock;
    private WeatherContext _context;
    private WeatherService _weatherService;

    public WeatherServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _configurationMock = new Mock<IConfiguration>();

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var options = new DbContextOptionsBuilder<WeatherContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase") // Create a new in-memory database for each test
            .Options;

        _context = new WeatherContext(options);
        _weatherService = new WeatherService(httpClientFactoryMock.Object, _configurationMock.Object, _context);
    }

    [Fact]
    public async Task GetWeatherData_SavesWeatherDataToDatabase_WhenCalledWithValidLocation()
    {
        // Arrange
        var location = "New York";
        var apiKey = "YOUR_API_KEY";
        _configurationMock.Setup(c => c["WeatherApi:ApiKey"]).Returns(apiKey);
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"main\":{\"temp\":280.32,\"feels_like\":277.51,\"temp_min\":279.15,\"temp_max\":281.48,\"pressure\":1016,\"humidity\":76},\"name\":\"New York\"}")
            });

        // Act
        var result = await _weatherService.GetWeatherData(location);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New York", result.Name);

        // Check if the weather data was saved to the database
        var savedWeatherData = _context.WeatherData.FirstOrDefault(w => w.Name == location);
        Assert.NotNull(savedWeatherData);
        Assert.Equal(result.Main.Temp, savedWeatherData.Main.Temp);
    }
}
