using web.Data;
using web.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AccessContext context)
        {
            context.Database.EnsureCreated();

            // Look for any clients.
            if (context.Sensor.Any())
            {
                return;   // DB has been seeded
            }

            //add roles
            var roles = new IdentityRole[] {
                new IdentityRole{Id="1", Name="Administrator", NormalizedName="Admin"},
                new IdentityRole{Id="2", Name="Manager", NormalizedName="Mng"},
                new IdentityRole{Id="3", Name="Staff", NormalizedName="Staff"},
                new IdentityRole{Id="4", Name="User", NormalizedName="User"}

            };
            context.Roles.AddRange(roles);

            /*
            foreach (IdentityRole r in roles)
            {
                context.Roles.Add(r);
            }
            */
            var com = new Company{CompanyId = 3,CompanyName = "FRI3", Addrass = "Vecna pot111111"};

            context.Company.Add(com);
            context.SaveChanges();

            //add admin user
            var user1 = new ApplicationUser
            {
                UserName = "Admin@1",
                NormalizedUserName = "Admin@1",
                Email = "Admin@1",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                CompanyId = com.CompanyId
            };

            if (!context.Users.Any(u => u.UserName == user1.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user1,"123");
                user1.PasswordHash = hashed;
                context.Users.Add(user1);
                
            }
            
            context.SaveChanges();

            var userRoles1 = new IdentityUserRole<string>[]{
                new IdentityUserRole<string>{RoleId = "1", UserId = user1.Id},
                new IdentityUserRole<string>{RoleId = "2", UserId = user1.Id},
                new IdentityUserRole<string>{RoleId = "3", UserId = user1.Id},
                new IdentityUserRole<string>{RoleId = "4", UserId = user1.Id}
            };
            context.UserRoles.AddRange(userRoles1);
            context.SaveChanges();

            //add Manager user
            var user2 = new ApplicationUser
            {
                UserName = "Manager@1",
                NormalizedUserName = "Manager@1",
                Email = "Manager@1",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                CompanyId = com.CompanyId
            };

            if (!context.Users.Any(u => u.UserName == user2.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user2,"123");
                user2.PasswordHash = hashed;
                context.Users.Add(user2);
            }
            context.SaveChanges();

            var userRoles2 = new IdentityUserRole<string>[]{
                new IdentityUserRole<string>{RoleId = "4", UserId = user2.Id},
                new IdentityUserRole<string>{RoleId = "2", UserId = user2.Id},
                new IdentityUserRole<string>{RoleId = "3", UserId = user2.Id}
            };
            context.UserRoles.AddRange(userRoles2);
            context.SaveChanges();


            //add Staff user
            var user3 = new ApplicationUser
            {
                UserName = "Staff@1",
                NormalizedUserName = "Staff@1",
                Email = "Staff@1",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                CompanyId = com.CompanyId
            };

            if (!context.Users.Any(u => u.UserName == user3.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user3,"123");
                user3.PasswordHash = hashed;
                context.Users.Add(user3);
                
            }
            context.SaveChanges();

            var userRoles3 = new IdentityUserRole<string>[]{
                new IdentityUserRole<string>{RoleId = "4", UserId = user3.Id},
                new IdentityUserRole<string>{RoleId = "3", UserId = user3.Id}
            };
            context.UserRoles.AddRange(userRoles3);
            context.SaveChanges();

            var loc = new Location[]{
                new Location{LocationId = 0, Name = "Atlantis", Address = "Smartinska cesta 193 1000 Ljubljana"},
                new Location{LocationId = 1, Name = "?", Address = "?"}
            };

            context.Location.AddRange(loc);
            context.SaveChanges();




            var sensors = new Sensor[]{
                new Sensor{SensorId = 1, SensorName = "Glavni senzor", Type = "senHOCL", SerialNumber = "1", LocationId = loc[1].LocationId},
                new Sensor{SensorId = 2, SensorName = "A2", Type = "senHOCL", SerialNumber = "2", Location = loc[0]},
                new Sensor{SensorId = 3, SensorName = "A3", Type = "senHOCL", SerialNumber = "3"},
            };


            context.Sensor.AddRange(sensors);
            context.SaveChanges();

            





            /*
            //set admin role
            var UserRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{UserId=user.Id, RoleId = roles[0].Id},
                new IdentityUserRole<string>{UserId=user.Id, RoleId = roles[1].Id}
            };

            foreach (IdentityUserRole<string> r in UserRoles)
            {
                context.UserRoles.Add(r);
            }
            context.SaveChanges();
            */

            
        }
    }
}