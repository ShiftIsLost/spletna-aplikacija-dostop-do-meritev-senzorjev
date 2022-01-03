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
            var user = new ApplicationUser
            {
                UserName = "1@1",
                NormalizedUserName = "1@1",
                Email = "1@1",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                CompanyId = com.CompanyId
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user,"123");
                user.PasswordHash = hashed;
                context.Users.Add(user);
                
            }
            
            context.SaveChanges();

            var loc = new Location[]{
                new Location{LocationId = 0, Name = "funny man", Address = "green man"},
                new Location{LocationId = 1, Name = "no funny man", Address = "green manaaaaaaaaa"}
            };

            context.Location.AddRange(loc);
            context.SaveChanges();




            var sensors = new Sensor[]{
                new Sensor{SensorId = 1, SensorName = "funny", Type = "senHOCL", SerialNumber = "1", LocationId = loc[1].LocationId},
                new Sensor{SensorId = 2, SensorName = "funny2", Type = "senHOCL", SerialNumber = "2", Location = loc[0]},
                new Sensor{SensorId = 3, SensorName = "funny3", Type = "senHOCL", SerialNumber = "3"},
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