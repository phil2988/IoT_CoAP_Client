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

            var dataToSeed = new List<Measurement>()
            {
                new Measurement()
                {
                    TimeMeasured= DateTime.Now.AddDays(-1),
                    SensorMeasurements = new List<SensorMeasurement>() {
                        new SensorMeasurement() { SensorNumber = 1, Temperature = 33 },
                        new SensorMeasurement() { SensorNumber = 2, Temperature = 22 },
                        new SensorMeasurement() { SensorNumber = 3, Temperature = 15 },
                        new SensorMeasurement() { SensorNumber = 4, Temperature = 27 },
                    }
                },
                new Measurement()
                {
                    TimeMeasured= DateTime.Now.AddDays(-2),
                    SensorMeasurements = new List<SensorMeasurement>() {
                        new SensorMeasurement () { SensorNumber = 1, Temperature = 26 },
                        new SensorMeasurement () { SensorNumber = 2, Temperature = 24 },
                        new SensorMeasurement () { SensorNumber = 3, Temperature = 16 },
                        new SensorMeasurement () { SensorNumber = 4, Temperature = 33 },
                    }
                },
                new Measurement()
                {
                    TimeMeasured = DateTime.Now.AddDays(-3),
                    SensorMeasurements = new List<SensorMeasurement>() {
                        new SensorMeasurement () { SensorNumber = 1, Temperature = 25 },
                        new SensorMeasurement () { SensorNumber = 2, Temperature = 28 },
                        new SensorMeasurement () { SensorNumber = 3, Temperature = 22 },
                        new SensorMeasurement () { SensorNumber = 4, Temperature = 21 },
                    }
                },
                new Measurement()
                {
                    TimeMeasured = DateTime.Now.AddDays(-4),
                    SensorMeasurements = new List<SensorMeasurement>() {
                        new SensorMeasurement () { SensorNumber = 1, Temperature = 22 },
                        new SensorMeasurement () { SensorNumber = 2, Temperature = 41 },
                        new SensorMeasurement () { SensorNumber = 3, Temperature = 12 },
                        new SensorMeasurement () { SensorNumber = 4, Temperature = 13 },
                    }
                }
            };

            Measurements.AddRange(dataToSeed);
            SaveChanges();
        }
    }
}
