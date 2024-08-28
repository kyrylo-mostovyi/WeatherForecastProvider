using Newtonsoft.Json;
using WeatherForecastProvider.Configuration;
using WeatherForecastProvider.Services;

namespace WeatherForecastProvider.DataStorage
{
  public interface IFileStorage
  {
    void StoreData(List<WeatherForecastModel> data);
  }
  public class FileStorageProvider : IFileStorage
  {
    private readonly ForecastConfiguration _configuration;

    public FileStorageProvider(ForecastConfiguration configuration)
    {
      _configuration = configuration;
    }
    public void StoreData(List<WeatherForecastModel> data)
    {
      string jsonFile = JsonConvert.SerializeObject(data);

      Directory.CreateDirectory(_configuration.FilePath);

      string path = _configuration.FilePath + $"/weatherForecast_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.json";

      File.WriteAllText(path, jsonFile);
    }
  }
}
