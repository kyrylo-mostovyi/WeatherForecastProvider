
using Microsoft.EntityFrameworkCore;
using WeatherForecastProvider.Services;

namespace WeatherForecastProvider.DataStorage
{
  public interface IDbStorage
  {
    IEnumerable<WeatherForecastModel> StoreData(List<WeatherForecastModel> data);
  }

  public class DbStorageProvider : IDbStorage
  {
    private readonly ForecastContext _context;
    private readonly IWeatherForecastDatabaseMapper _mapper;

    public DbStorageProvider(ForecastContext context, IWeatherForecastDatabaseMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }
    public IEnumerable<WeatherForecastModel> StoreData(List<WeatherForecastModel> data)
    {
      using (var db = _context)
      {
        var latestForecasts = db.WeatherForecasts
          .GroupBy(f => f.AirportCode, (key, group) => group.OrderByDescending(wf => wf.IssueTime).First())
          .ToDictionary(wf => wf.AirportCode, wf => wf.IssueTime);

        var addedForecasts = new List<WeatherForecastModel>();

        foreach (var model in data)
        {
          if (latestForecasts.TryGetValue(model.AirportCode, out var issueTime))
          {
            if (model.IssueTime > issueTime)
            {
              addedForecasts.Add(model);
            }
          }

          addedForecasts.Add(model);
        }

        db.WeatherForecasts.AddRange(_mapper.MapToDbModel(addedForecasts));
        db.SaveChanges();
        return addedForecasts;
      }
    }
  }
}
