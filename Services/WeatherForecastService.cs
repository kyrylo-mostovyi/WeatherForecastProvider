using System.IO;
using System.Net.Http.Json;
using System.Text;
using WeatherForecastProvider.Configuration;

namespace WeatherForecastProvider.Services
{
  public interface IWeatherForecastService
  {
    Task<IEnumerable<WeatherForecastModel>> GetWeatherForecastAsync(string airports);
  }

  public class WeatherForecastService : IWeatherForecastService
  {
    private readonly ForecastConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly IWeatherForecastMapper _mapper;
    public WeatherForecastService(ForecastConfiguration configuration, IWeatherForecastMapper mapper)
    {
      _configuration = configuration;
      _httpClient = new HttpClient();
      _mapper = mapper;
    }
    public async Task<IEnumerable<WeatherForecastModel>> GetWeatherForecastAsync(string airportCodes)
    {

      var queryString = new StringBuilder(_configuration.WeatherURI);
      queryString.Append("?ids=")
        .Append(airportCodes)
        .Append("&format=json");

      var forecasts = new List<WeatherForecastDto>();

      HttpResponseMessage response = await _httpClient.GetAsync(queryString.ToString());
      
      if (response.IsSuccessStatusCode)
      {
        forecasts = await response.Content.ReadFromJsonAsync<List<WeatherForecastDto>>();
      }

      if (forecasts == null ||  forecasts.Count == 0)
      {
        return new List<WeatherForecastModel>();
      }

      return _mapper.ConvertToModels(forecasts);
    }
  }
}
