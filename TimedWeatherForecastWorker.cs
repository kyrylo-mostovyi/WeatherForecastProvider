using WeatherForecastProvider.Configuration;
using WeatherForecastProvider.DataStorage;
using WeatherForecastProvider.Services;

namespace WeatherForecastProvider
{
  public class TimedWeatherForecastWorker : IHostedService, IDisposable
  {
    private readonly IWeatherForecastService _weatherForecastService;
    private readonly IDataStorage _dataStorage;
    private readonly ForecastConfiguration _configuration;
    private Timer? _timer = null;

    public TimedWeatherForecastWorker(ForecastConfiguration config, IWeatherForecastService weatherForecastService, IDataStorage dataStorage)
    {
      _configuration = config;
      _weatherForecastService = weatherForecastService;
      _dataStorage = dataStorage;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
      Console.WriteLine("Started weather forecast service.");

      _timer = new Timer(DoWork, null, TimeSpan.Zero,
          TimeSpan.FromSeconds(_configuration.Seconds));

      return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
      var forecast = await _weatherForecastService.GetWeatherForecastAsync(_configuration.AirportCodes);
      if (forecast != null && forecast.Any())
      {
        _dataStorage.StoreData(forecast.ToList());
      }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
      Console.WriteLine("Weather forecast service is stopping.");

      _timer?.Change(Timeout.Infinite, 0);

      return Task.CompletedTask;
    }

    public void Dispose()
    {
      _timer?.Dispose();
    }
  }
}
