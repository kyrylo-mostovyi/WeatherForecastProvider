using WeatherForecastProvider.Configuration;
using WeatherForecastProvider.DataStorage;
using WeatherForecastProvider.Services;

namespace WeatherForecastProvider
{
  public class TimedWeatherForecastWorker : IHostedService, IDisposable
  {
    private readonly IWeatherForecastService _weatherForecastService;
    private readonly IWeatherForecastIssueTimeFilteringService _weatherForecastIssueTimeFilteringService;
    private readonly IDataStorage _dataStorage;
    private readonly ForecastConfiguration _configuration;
    private Timer? _timer = null;

    public TimedWeatherForecastWorker(ForecastConfiguration config, 
      IWeatherForecastService weatherForecastService, 
      IDataStorage dataStorage, 
      IWeatherForecastIssueTimeFilteringService weatherForecastIssueTimeFilteringService)
    {
      _configuration = config;
      _weatherForecastService = weatherForecastService;
      _dataStorage = dataStorage;
      _weatherForecastIssueTimeFilteringService = weatherForecastIssueTimeFilteringService;
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
      var forecasts = await _weatherForecastService.GetWeatherForecastAsync(_configuration.AirportCodes);

      var fileredFoercasts = _weatherForecastIssueTimeFilteringService.GetForecastsWithNewIssueTime(forecasts);
      
      if (fileredFoercasts != null && fileredFoercasts.Any())
      {
        _dataStorage.StoreData(forecasts.ToList());
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
