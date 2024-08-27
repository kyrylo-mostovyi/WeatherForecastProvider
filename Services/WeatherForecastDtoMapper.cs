
namespace WeatherForecastProvider.Services
{
  public interface IWeatherForecastMapper
  {
    List<WeatherForecastModel> ConvertToModels(List<WeatherForecastDto> dtos);
  }
  public class WeatherForecastDtoMapper : IWeatherForecastMapper
  {
    public List<WeatherForecastModel> ConvertToModels(List<WeatherForecastDto> dtos)
    {
      var models = new List<WeatherForecastModel>();

      foreach (WeatherForecastDto dto in dtos)
      {
        var model = new WeatherForecastModel();
        model.AirportCode = dto.IcaoId;
        model.RawTAF = dto.RawTAF;
        model.IssueTime = DateTime.Parse(dto.IssueTime).ToLocalTime();
        model.ValidFrom = ConvertUnixTimeStampToDateTime(dto.ValidTimeFrom);
        model.ValidTo = ConvertUnixTimeStampToDateTime(dto.ValidTimeTo);
        models.Add(model);
      }

      return models;
    }

    private DateTime ConvertUnixTimeStampToDateTime(int unixTimeStamp)
    {
      DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
      return dateTime;
    }
  }
}
