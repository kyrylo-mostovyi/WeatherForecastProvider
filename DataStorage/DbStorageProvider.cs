
using Microsoft.EntityFrameworkCore;
using WeatherForecastProvider.Services;

namespace WeatherForecastProvider.DataStorage
{
  public class DbStorageProvider : IDataStorage
  {
    private readonly ForecastContext _context;
    private readonly IWeatherForecastDatabaseMapper _mapper;

    public DbStorageProvider(ForecastContext context, IWeatherForecastDatabaseMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }
    public void StoreData(List<WeatherForecastModel> data)
    {
      using (var db = _context)
      {
        db.WeatherForecasts.AddRange(_mapper.MapToDbModel(data));
        var addedEntities = db.SaveChanges();
        Console.WriteLine($"{addedEntities} entities were added to db");
      }
    }
  }
}
