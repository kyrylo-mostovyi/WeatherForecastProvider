
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeatherForecastProvider.Services
{
  public class WeatherForecastDto
  {
    public string IcaoId { get; set; } = string.Empty;
    public string IssueTime { get; set; } = string.Empty;
    public int ValidTimeFrom { get; set; }
    public int ValidTimeTo { get; set; }
    public string RawTAF { get; set; } = string.Empty;
  }
}
