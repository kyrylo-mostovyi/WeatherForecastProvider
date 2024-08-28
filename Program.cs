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
              services.AddSingleton<IIssueTimeChecker, IssueTimeChecker>();
              services.AddSingleton<IWeatherForecastIssueTimeFilteringService, WeatherForecastIssueTimeFilteringService>();

              if (forecastConfig.StoreToDb)
              {
                services.AddScoped<IDataStorage, DbStorageProvider>();
              }
              else
              {
                services.AddScoped<IDataStorage, FileStorageProvider>();
              }

              services.AddHostedService<TimedWeatherForecastWorker>();
            });
  }
}