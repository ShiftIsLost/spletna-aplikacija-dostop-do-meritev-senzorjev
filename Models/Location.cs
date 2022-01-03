using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace web.Models
{
    public class Location 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LocationId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double? x { get; set; }
        public double? y { get; set; }
        public ICollection<Sensor>? Sensor { get; set; }
    }
}