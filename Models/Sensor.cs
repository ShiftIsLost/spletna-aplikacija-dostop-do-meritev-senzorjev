using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace web.Models
{
    public class Sensor 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SensorId { get; set; }   
        public int? LocationId { get; set; }// reference 
        public string? SensorName { get; set; } 
        public string Type { get; set; }// senHOCL, senORP, senPH...
        public string SerialNumber { get; set; }
        public string? FirmwareVersion { get; set; }
        public Location? Location { get; set; }
        public ICollection<UserSensor>? UserSensor { get; set; }

    }
}