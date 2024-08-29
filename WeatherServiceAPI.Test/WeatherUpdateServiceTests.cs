using Moq;
using WeatherServiceAPI.Data;
using WeatherServiceAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace WeatherServiceAPI.Test;
public class WeatherUpdateServiceTests
{
    private Mock<WeatherService> _weatherServiceMock;
    private WeatherUpdateService _weatherUpdateService;

    public WeatherUpdateServiceTests()
    {
        var httpClientMock = new Mock<HttpClient>();
        var configurationMock = new Mock<IConfiguration>();
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);

        var options = new DbContextOptionsBuilder<WeatherContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase") // Create a new in-memory database for each test
            .Options;
        var context = new WeatherContext(options);

        _weatherServiceMock = new Mock<WeatherService>(httpClientFactoryMock.Object, configurationMock.Object, context);
        _weatherUpdateService = new WeatherUpdateService(_weatherServiceMock.Object);
    }

    [Fact]
    public async Task StartAsync_CallsGetWeatherData_WhenCalled()
    {
        // Arrange
        var location = "New York";
        _weatherServiceMock.Setup(s => s.GetWeatherData(location)).ReturnsAsync(new WeatherData());

        // Act
        await _weatherUpdateService.StartAsync(CancellationToken.None);

        // Assert
        _weatherServiceMock.Verify(s => s.GetWeatherData(location), Times.Once);
    }
}

