namespace WeatherForecastProvider.Configuration
{
  public class ForecastConfiguration
  {
    public string WeatherURI { get; set; } = string.Empty;
    public bool StoreToDb { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string DbConnection { get; set; } = string.Empty;
    public string AirportCodes { get; set; } = string.Empty;
  }
}
