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
                new Client{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2019-09-01")},
                new Client{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Client{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2018-09-01")},
                new Client{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Client{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Client{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2016-09-01")},
                new Client{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2018-09-01")},
                new Client{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2019-09-01")}
            };

            context.Clients.AddRange(clients);
            context.SaveChanges();

            var sensors = new Sensor[]
            {
                new Sensor{SensorID=1050,SerialNumber="",Type="",Location="",FirmwareVersion=""},
                new Sensor{SensorID=4022,SerialNumber="",Type="",Location="",FirmwareVersion=""},
                new Sensor{SensorID=4041,SerialNumber="",Type="",Location="",FirmwareVersion=""},
                new Sensor{SensorID=1045,SerialNumber="",Type="",Location="",FirmwareVersion=""},
                new Sensor{SensorID=3141,SerialNumber="",Type="",Location="",FirmwareVersion=""},
                new Sensor{SensorID=2021,SerialNumber="",Type="",Location="",FirmwareVersion=""},
                new Sensor{SensorID=2042,SerialNumber="",Type="",Location="",FirmwareVersion=""}
            };

            context.Sensors.AddRange(sensors);
            context.SaveChanges();

            var accesses = new SensorAccess[]
            {
                new SensorAccess{ClientID=1,SensorID=1050},
                new SensorAccess{ClientID=1,SensorID=4022},
                new SensorAccess{ClientID=1,SensorID=4041},
                new SensorAccess{ClientID=2,SensorID=1045},
                new SensorAccess{ClientID=2,SensorID=3141},
                new SensorAccess{ClientID=2,SensorID=2021},
                new SensorAccess{ClientID=3,SensorID=1050},
                new SensorAccess{ClientID=4,SensorID=1050},
                new SensorAccess{ClientID=4,SensorID=4022},
                new SensorAccess{ClientID=5,SensorID=4041},
                new SensorAccess{ClientID=6,SensorID=1045},
                new SensorAccess{ClientID=7,SensorID=3141},
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
                City = "Ljubljana",
                Email = "bob@example.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "bob@example.com",
                NormalizedUserName = "bob@example.com",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ClientID = 1
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