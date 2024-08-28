using WeatherForecastProvider.Configuration;
using WeatherForecastProvider.DataStorage;
using WeatherForecastProvider.Services;

namespace WeatherForecastProvider
{
    public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) => {
              
              IConfiguration configuration = hostContext.Configuration;

              ForecastConfiguration forecastConfig = configuration.GetSection("ForecastConfig").Get<ForecastConfiguration>();

              services.AddSingleton(forecastConfig);
              services.AddScoped<ForecastContext>();
              services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
              services.AddSingleton<IWeatherForecastMapper, WeatherForecastDtoMapper>();
              services.AddSingleton<IWeatherForecastDatabaseMapper, WeatherForecastDatabaseMapper>();
              services.AddScoped<IDbStorage, DbStorageProvider>();
              services.AddScoped<IFileStorage, FileStorageProvider>();
              services.AddHostedService<TimedWeatherForecastWorker>();
            });
  }
}