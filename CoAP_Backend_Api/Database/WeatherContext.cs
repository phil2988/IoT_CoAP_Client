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
                    Temperature = 33
                },
                new Measurement
                {
                    Temperature = 12
                },
                new Measurement
                {
                    Temperature = 21.52
                },
                new Measurement
                {
                    Temperature = 16.52
                },
                new Measurement
                {
                    Temperature = 14.27
                },
                new Measurement
                {
                    Temperature = 32.53
                }
            );
            SaveChanges();
        }
    }
}
