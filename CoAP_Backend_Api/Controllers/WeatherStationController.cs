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
        private CoapClient client;
        public WeatherStationController(IWeatherStationServices services)
        {
            client = new CoapClient();
            client.Uri = new Uri("coap://192.168.137.149/Espressif");
            this.services = services;
        }

        [HttpGet()]
        public async Task<ActionResult<Measurement>> GetData()
        {
            List<string> responses = new List<string>();
            var response = client.Get().PayloadString;
            if(response == null)
            {
                return NotFound("Could not read values from sensor");
            }
            var values = response.Split(',');
            foreach (var value in values)
            {
                responses.Add(value);
            }
            var returnValue = new Measurement { Temperature = double.Parse(responses[0]) };
            var res = await services.AddMeasurementToDb(returnValue);

            return Ok(new JsonResult(returnValue));
        }
    }
}