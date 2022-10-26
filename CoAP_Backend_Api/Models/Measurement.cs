using System.ComponentModel.DataAnnotations;

namespace CoAP_Backend_Api.Models
{
    public class Measurement
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Measured { get; set; } = DateTime.Now;
        public double Temperature { get; set; } = 0;
        public double Humidity { get; set; } = 0;
    }
}
