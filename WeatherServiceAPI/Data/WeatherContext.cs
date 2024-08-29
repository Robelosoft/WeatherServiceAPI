using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WeatherServiceAPI.Data;
public class WeatherContext : DbContext
{
    public WeatherContext(DbContextOptions<WeatherContext> options)
    : base(options)
    {
    }

    public DbSet<WeatherData> WeatherData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherData>()
            .HasKey(w => w.Id); // Assuming 'Id' is the primary key

        modelBuilder.Entity<WeatherData>()
            .Ignore(w => w.Main); // Ignore the 'Main' property
    }
}

