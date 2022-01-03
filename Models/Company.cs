using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace web.Models
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyId { get; set; }    
        public string? CompanyName { get; set; } 
        public string? Addrass { get; set; }
        public ICollection<ApplicationUser>? UserId { get; set; }

    }
}