using FluentAssertions;
using Moq;
using MyWebApi.Controllers;
using MyWebApi.Services;

namespace MyWebApi.Tests.Controllers;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsWeatherForecasts()
    {
        // 1. 模拟依赖项
        var mockService = new Mock<IWeatherService>();

        // 设置模拟返回值
        var testData = new[] {
            new WeatherForecast(DateOnly.FromDateTime(DateTime.Today), 25 ),
            new WeatherForecast(DateOnly.FromDateTime(DateTime.Today.AddDays(1)), 18)
        };

        mockService.Setup(s => s.GetForecast())
            .Returns(testData);

        // 2. 创建控制器实例
        var controller = new WeatherForecastController(mockService.Object);

        // 3. 调用API方法
        var result = controller.Get();

        // 4. 验证结果
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.First().TemperatureC.Should().Be(25);

        // 验证服务方法被调用
        mockService.Verify(s => s.GetForecast(), Times.Once);
    }
}