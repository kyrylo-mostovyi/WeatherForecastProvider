using WeatherForecastProvider.Services;

namespace WeatherForecastProvider.DataStorage
{
  public interface IWeatherForecastDatabaseMapper
  {
    IEnumerable<WeatherForecastDbModel> MapToDbModel(List<WeatherForecastModel> forecasts);
  }
  public class WeatherForecastDatabaseMapper : IWeatherForecastDatabaseMapper
  {
    public IEnumerable<WeatherForecastDbModel> MapToDbModel(List<WeatherForecastModel> forecasts)
    {
      var dbModels = new List<WeatherForecastDbModel>();
      
      foreach (var model in forecasts) 
      {
        dbModels.Add(
          new WeatherForecastDbModel
          {
            AirportCode = model.AirportCode,
            IssueTime = model.IssueTime,
            ValidFrom = model.ValidFrom,
            ValidTo = model.ValidTo,
            RawTAF = model.RawTAF
          });
      }

      return dbModels;
    }
  }
}
