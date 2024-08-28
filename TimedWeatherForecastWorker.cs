using WeatherForecastProvider.Configuration;
using WeatherForecastProvider.DataStorage;
using WeatherForecastProvider.Services;

namespace WeatherForecastProvider
{
  public class TimedWeatherForecastWorker : BackgroundService
  {
    public IServiceProvider Services { get; }
    private readonly ForecastConfiguration _configuration;

    public TimedWeatherForecastWorker(IServiceProvider services, ForecastConfiguration config)
    {
      Services = services;
      _configuration = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      Console.WriteLine("Started weather forecast service.");

      await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {     
      using (var scope = Services.CreateScope())
      {
        var weatherForecastService =
            scope.ServiceProvider
                .GetRequiredService<IWeatherForecastService>();

        var forecasts = await weatherForecastService.GetWeatherForecastAsync(_configuration.AirportCodes);

        var dbStorage = scope.ServiceProvider
                .GetRequiredService<IDbStorage>();
        
        var storedForecasts = dbStorage.StoreData(forecasts.ToList());

        if (storedForecasts != null && storedForecasts.Any() && _configuration.StoreToFile)
        {
          var dataStorage = scope.ServiceProvider
                .GetRequiredService<IFileStorage>();

          dataStorage.StoreData(storedForecasts.ToList());
        }
      }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
      await base.StopAsync(stoppingToken);
    }
  }
}
