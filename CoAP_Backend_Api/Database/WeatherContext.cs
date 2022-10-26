using CoAP_Backend_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CoAP_Backend_Api.Database
{
    public class WeatherContext:DbContext
    {
        public DbSet<Measurement> Measurements { get; set; }

        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options)
        {
            Database.EnsureCreated();

            if (Measurements != null && !Measurements.Any())
            {
                SeedMeasurements();
            }
        }

        void SeedMeasurements()
        {
            if(Measurements == null)
            {
                return;
            }

            Measurements.AddRange(
                new Measurement { 
                    Humidity = 12,
                    Temperature = 33
                },
                new Measurement
                {
                    Humidity = 22,
                    Temperature = 12
                },
                new Measurement
                {
                    Humidity = 56.23,
                    Temperature = 21.52
                },
                new Measurement
                {
                    Humidity = 80.84,
                    Temperature = 16.52
                },
                new Measurement
                {
                    Humidity = 67.39,
                    Temperature = 14.27
                },
                new Measurement
                {
                    Humidity = 99.99,
                    Temperature = 32.53
                }
            );
            SaveChanges();
        }
    }
}
