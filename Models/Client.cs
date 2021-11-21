using System;
using System.Collections.Generic;

namespace web.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrgType { get; set; }
        public int OrgId { get; set; }


        public ICollection<SensorAccess> Accesses { get; set; } 
    }
}