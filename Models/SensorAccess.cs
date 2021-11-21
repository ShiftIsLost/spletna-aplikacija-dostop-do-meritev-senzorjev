namespace web.Models
{

    public class SensorAccess
    {
        public int AccessId { get; set; }
        public int ClientId { get; set; }
        public int SensorId { get; set; }

        public Sensor Sensor { get; set; }
        public Client Client { get; set; }
    }
}