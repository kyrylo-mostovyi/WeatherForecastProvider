using Newtonsoft.Json;
using WeatherForecastProvider.Configuration;
using WeatherForecastProvider.Services;

namespace WeatherForecastProvider.DataStorage
{
  public class FileStorageProvider : IDataStorage
  {
    private readonly ForecastConfiguration _configuration;

    public FileStorageProvider(ForecastConfiguration configuration)
    {
      _configuration = configuration;
    }
    public void StoreData(List<WeatherForecastModel> data)
    {
      string jsonFile = JsonConvert.SerializeObject(data);

      string path = _configuration.FilePath + $"weatherForecast_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.json";
      
      File.WriteAllText(path, jsonFile);
    }
  }
}
