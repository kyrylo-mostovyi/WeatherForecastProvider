namespace WeatherForecastProvider.Services
{
  public class WeatherForecastModel
  {
    public string AirportCode { get; set; } = string.Empty;
    public DateTime IssueTime { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public string RawTAF { get; set; } = string.Empty;
  }
}
