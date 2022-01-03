namespace web.Models
{
    public class UserSensor 
    {
        public int UserSensorId { get; set; }
        public int SensorId { get; set; }
        public string ApplicationUserId { get; set; }
        public Sensor? Sensor { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

    }
}