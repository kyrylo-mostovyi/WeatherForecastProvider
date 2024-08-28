using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastProvider.Services
{
  public interface IWeatherForecastIssueTimeFilteringService
  {
    IEnumerable<WeatherForecastModel> GetForecastsWithNewIssueTime(IEnumerable<WeatherForecastModel> forecasts);
  }


  public class WeatherForecastIssueTimeFilteringService : IWeatherForecastIssueTimeFilteringService
  {
    private readonly IIssueTimeChecker _issueTimeChecker;
    public WeatherForecastIssueTimeFilteringService(IIssueTimeChecker issueTimeChecker) 
    {
      _issueTimeChecker = issueTimeChecker;
    }
    public IEnumerable<WeatherForecastModel> GetForecastsWithNewIssueTime(IEnumerable<WeatherForecastModel> forecasts)
    {
      return forecasts.Where(f => _issueTimeChecker.IsTheSameIssueTime(f.AirportCode, f.IssueTime));
    }
  }
}
