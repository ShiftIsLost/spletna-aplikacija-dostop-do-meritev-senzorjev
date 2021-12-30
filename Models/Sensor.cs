using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Sensor
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SensorId { get; set; }    
        public string SerialNumber { get; set; }
        public string? Location { get; set; }
        public string? Type { get; set; }
        public string? FirmwareVersion { get; set; }

        public ICollection<SensorAccess>? Accesses { get; set; } 
    }
}