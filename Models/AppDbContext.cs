using Microsoft.EntityFrameworkCore;
namespace Project_WebAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<EmployeeMaster> Employee { get; set; }
        public DbSet<productL> productL { get; set; }
        public DbSet<login> login { get; set; }

    }
}
