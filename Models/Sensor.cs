using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Sensor
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SensorID { get; set; }    
        public ApplicationUser Owner { get; set; }

        public string SerialNumber { get; set; }
        public string Tpye { get; set; }
        public string FirmwareVersion { get; set; }
    }
}