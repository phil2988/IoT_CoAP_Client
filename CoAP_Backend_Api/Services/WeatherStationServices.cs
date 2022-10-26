using CoAP_Backend_Api.Models;
using CoAP_Backend_Api.Repositories;

namespace CoAP_Backend_Api.Services
{
    public interface IWeatherStationServices
    {
        Task<int> AddMeasurementToDb(Measurement measurement);
    }
    public class WeatherStationServices: IWeatherStationServices
    {
        public IWeatherStationRepository Repository { get; set; }
        public WeatherStationServices(IWeatherStationRepository repository)
        {
            Repository = repository;
        }

        public async Task<int> AddMeasurementToDb(Measurement measurement)
        {
            return await Repository.Add(measurement);
        }
    }
}
