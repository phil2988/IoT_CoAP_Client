using CoAP;
using CoAP_Backend_Api.Models;
using CoAP_Backend_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoAP_Backend_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherStationController : ControllerBase
    {
        private readonly IWeatherStationServices services;
        private readonly CoapClient client;
        public WeatherStationController(IWeatherStationServices services)
        {
            client = new();
            client.Uri = new Uri("coap://192.168.137.42/Espressif");
            this.services = services;
        }
        
        [HttpGet("Newest")]
        public async Task<ActionResult<Measurement>> Newest()
        {

            // response is: id, measurement, id, measurement....
#if !DEBUG
            var response = client.Get().PayloadString;
#endif
#if DEBUG
            var response = "0, 23, 1, 23, 2, 20, 3, 18";
#endif

            if(response == null)
            {
                return NotFound("Could not read values from sensor");
            }

            var values = response.Split(',');
            List<string> ids = new();
            List<string> temps = new();

            // Splits ids and temps
            for (int i = 0; i < values.Length; i++)
            {
                if(i % 2 == 0)
                {
                    ids.Add(values[i]);
                }
                if (i % 2 != 0)
                {
                    temps.Add(values[i]);
                }
            }

            // Collects sensor numbers and temperatures
            List<SensorMeasurement> data = new();
            for (int i = 0; i < ids.Count; i++)
            {
                data.Add(new SensorMeasurement
                {
                    SensorNumber = int.Parse(ids[i]),
                    Temperature = double.Parse(temps[i])
                });
            }

            var measurement = new Measurement { SensorMeasurements = data };
            await services.AddMeasurementToDb(measurement);

            // Frontend needs measurements in an array
            return Ok(new JsonResult(new Measurement[] { measurement }));
        }

        [HttpGet("All")]
        public ActionResult<Measurement> All()
        {
            var response = services.GetAllMeasurements();

            if(response == null)
            {
                return NotFound("Could not read measurements from database");
            }

            return Ok(new JsonResult(response));
        }
    }
}