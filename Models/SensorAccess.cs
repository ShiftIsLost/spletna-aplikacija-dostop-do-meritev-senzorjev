namespace web.Models
{

    public class SensorAccess
    {
        public int AccessID { get; set; }
        public int ClientID { get; set; }
        public int SensorID { get; set; }

        public Sensor Sensor { get; set; }
        public Client Client { get; set; }
    }
}