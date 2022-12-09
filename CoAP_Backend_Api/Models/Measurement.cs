using System.ComponentModel.DataAnnotations;

namespace CoAP_Backend_Api.Models
{
    public class Measurement
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime TimeMeasured { get; set; } = DateTime.Now;

        public ICollection<SensorMeasurement> SensorMeasurements { get; set; } = new List<SensorMeasurement>();
    }

    public class SensorMeasurement
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int SensorNumber { get; set; } = 0;
        public double Temperature { get; set; }
    }
}
