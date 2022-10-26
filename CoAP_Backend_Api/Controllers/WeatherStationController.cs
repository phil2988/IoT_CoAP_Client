using CoAP;
using CoAP_Backend_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoAP_Backend_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherStationController : ControllerBase
    {
        private CoapClient client;
        public WeatherStationController()
        {
            client = new CoapClient();
            client.Uri = new Uri("coap://192.168.137.149/Espressif");
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
            return Ok(new Measurement { Temperature = double.Parse(responses[0])});
        }
    }
}