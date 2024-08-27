using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WeatherForecastProvider.Configuration;
using WeatherForecastProvider.Services;

namespace WeatherForecastProvider.DataStorage
{
  public class ForecastContext : DbContext
  {
    public DbSet<WeatherForecastDbModel> WeatherForecasts { get; set; }
    public string DbPath { get; }

    public ForecastContext(ForecastConfiguration config)
    {
      DbPath = config.DbConnection;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
  }

  public class WeatherForecastDbModel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int WeatherForecastId { get; set; }
    public string AirportCode { get; set; } = string.Empty;
    public DateTime IssueTime { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public string RawTAF { get; set; } = string.Empty;
  }
}
