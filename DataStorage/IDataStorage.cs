using WeatherForecastProvider.Services;

namespace WeatherForecastProvider.DataStorage
{
  public interface IDataStorage
  {
    void StoreData(List<WeatherForecastModel> data);
  }
}
