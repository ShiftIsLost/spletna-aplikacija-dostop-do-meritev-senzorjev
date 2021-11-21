using Microsoft.AspNetCore.Identity;

namespace web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}