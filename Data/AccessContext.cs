using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public class AccessContext : IdentityDbContext<ApplicationUser>
    {
        public AccessContext(DbContextOptions<AccessContext> options) : base(options)
        {
        }

        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorAccess> Accesses { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Sensor>().ToTable("Sensor");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<SensorAccess>().ToTable("SensorAccess");
            modelBuilder.Entity<SensorAccess>().HasKey(t => t.AccessId);
            //modelBuilder.Entity<SensorAccess>().HasMany(t => t.Sensor).WithMany(t => t.Client);
        }
    }
}