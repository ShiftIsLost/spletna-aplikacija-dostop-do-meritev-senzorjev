using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public class AccessContext : IdentityDbContext<ApplicationUser>
    {
        public AccessContext (DbContextOptions<AccessContext> options)
            : base(options)
        {
        }

        public DbSet<web.Models.Company> Company { get; set; }
        public DbSet<web.Models.Sensor> Sensor { get; set; }
        public DbSet<web.Models.UserSensor> UserSensor { get; set; }
        public DbSet<web.Models.Location> Location { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Sensor>().ToTable("Sensor");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<UserSensor>().ToTable("UserSensor");
        }
    
    
       

    }
}
