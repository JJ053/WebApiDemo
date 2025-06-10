namespace MyWebApi.Services;

public interface IWeatherService
{
    IEnumerable<WeatherForecast> GetForecast();
}

public record WeatherForecast(DateOnly Date, int TemperatureC)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}