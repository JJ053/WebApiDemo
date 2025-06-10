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
        // 1. ģ��������
        var mockService = new Mock<IWeatherService>();

        // ����ģ�ⷵ��ֵ
        var testData = new[] {
            new WeatherForecast(DateOnly.FromDateTime(DateTime.Today), 25 ),
            new WeatherForecast(DateOnly.FromDateTime(DateTime.Today.AddDays(1)), 18)
        };

        mockService.Setup(s => s.GetForecast())
            .Returns(testData);

        // 2. ����������ʵ��
        var controller = new WeatherForecastController(mockService.Object);

        // 3. ����API����
        var result = controller.Get();

        // 4. ��֤���
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.First().TemperatureC.Should().Be(25);

        // ��֤���񷽷�������
        mockService.Verify(s => s.GetForecast(), Times.Once);
    }
}