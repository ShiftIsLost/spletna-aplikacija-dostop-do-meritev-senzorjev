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

            // Look for any students.
            if (context.Clients.Any())
            {
                return;   // DB has been seeded
            }

            var clients = new Client[]
            {
                new Client{FirstName="Franc",LastName="Novak",OrgType="",OrgId=0},
                new Client{FirstName="Marija",LastName="Horvat",OrgType="",OrgId=0},
                new Client{FirstName="Janez",LastName="Kovacic",OrgType="",OrgId=1},
                new Client{FirstName="Ana",LastName="Krajnc",OrgType="",OrgId=2},
                new Client{FirstName="Marko",LastName="Zupancic",OrgType="",OrgId=3},
                new Client{FirstName="Maja",LastName="Kovac",OrgType="",OrgId=4},
                new Client{FirstName="Andrej",LastName="Potocnik",OrgType="",OrgId=5},
                new Client{FirstName="Irena",LastName="Mlakar",OrgType="",OrgId=6}
            };

            context.Clients.AddRange(clients);
            context.SaveChanges();

            var sensors = new Sensor[]
            {
                new Sensor{SensorId=1050,SerialNumber="55143",Type="Temperatura",Location="Vhod",FirmwareVersion=""},
                new Sensor{SensorId=4022,SerialNumber="04101",Type="Klorometer",Location="Glavni bazen",FirmwareVersion=""},
                new Sensor{SensorId=4041,SerialNumber="66212",Type="Temperatura",Location="Tobogan",FirmwareVersion=""},
                new Sensor{SensorId=1045,SerialNumber="08990",Type="Klorometer",Location="Tobogan",FirmwareVersion=""},
                new Sensor{SensorId=3141,SerialNumber="41769",Type="Temperatura",Location="Savna",FirmwareVersion=""},
                new Sensor{SensorId=2021,SerialNumber="83990",Type="Zasedenost",Location="Glavni bazen",FirmwareVersion=""},
                new Sensor{SensorId=2042,SerialNumber="10088",Type="Visina",Location="Glavni bazen",FirmwareVersion=""}
            };

            context.Sensors.AddRange(sensors);
            context.SaveChanges();

            var accesses = new SensorAccess[]
            {
                new SensorAccess{ClientId=1,SensorId=1050},
                new SensorAccess{ClientId=1,SensorId=4022},
                new SensorAccess{ClientId=1,SensorId=4041},
                new SensorAccess{ClientId=2,SensorId=1045},
                new SensorAccess{ClientId=2,SensorId=3141},
                new SensorAccess{ClientId=2,SensorId=2021},
                new SensorAccess{ClientId=3,SensorId=1050},
                new SensorAccess{ClientId=4,SensorId=1050},
                new SensorAccess{ClientId=4,SensorId=4022},
                new SensorAccess{ClientId=5,SensorId=4041},
                new SensorAccess{ClientId=6,SensorId=1045},
                new SensorAccess{ClientId=7,SensorId=3141},
            };

            context.Accesses.AddRange(accesses);

            var roles = new IdentityRole[] {
                new IdentityRole{Id="1", Name="Administrator"},
                new IdentityRole{Id="2", Name="Manager"},
                new IdentityRole{Id="3", Name="Staff"}
            };

            foreach (IdentityRole r in roles)
            {
                context.Roles.Add(r);
            }

            var user = new ApplicationUser
            {
                FirstName = "Bob",
                LastName = "Dilon",
                Email = "bob@example.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "bob@example.com",
                NormalizedUserName = "bob@example.com",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ClientId = 1
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user,"Testni123!");
                user.PasswordHash = hashed;
                context.Users.Add(user);
                
            }

            context.SaveChanges();
            

            var UserRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=user.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user.Id},
            };

            foreach (IdentityUserRole<string> r in UserRoles)
            {
                context.UserRoles.Add(r);
            }



            context.SaveChanges();
        }
    }
}