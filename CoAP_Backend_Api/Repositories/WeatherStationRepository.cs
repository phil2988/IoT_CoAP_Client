﻿using CoAP_Backend_Api.Database;
using CoAP_Backend_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CoAP_Backend_Api.Repositories
{
    public interface IWeatherStationRepository
    {
        Task<int> Add(Measurement measurement);
        List<Measurement> GetAll();
    }
    public class WeatherStationRepository: IWeatherStationRepository
    {
        private readonly WeatherContext dbContext;

        public WeatherStationRepository(WeatherContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Add(Measurement measurement)
        {
            dbContext.Measurements.Add(measurement);
            var result = await dbContext.SaveChangesAsync();
            return result;
        }

        public List<Measurement> GetAll()
        {
            return dbContext.Measurements
                .Include((m) => m.SensorMeasurements)
                .ToList();
        }
    }
}
