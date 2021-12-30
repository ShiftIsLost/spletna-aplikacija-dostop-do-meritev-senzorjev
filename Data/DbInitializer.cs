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
            if (context.Clients.Any())
            {
                return;   // DB has been seeded
            }

            //add admin client
            var clients = new Client[]
            {
                new Client{FirstName="Jan",LastName="Hribar",OrgType="Admin",OrgId=0}/*,
                new Client{FirstName="Marija",LastName="Horvat",OrgType="",OrgId=0},
                new Client{FirstName="Janez",LastName="Kovacic",OrgType="",OrgId=1},
                new Client{FirstName="Ana",LastName="Krajnc",OrgType="",OrgId=2},
                new Client{FirstName="Marko",LastName="Zupancic",OrgType="",OrgId=3},
                new Client{FirstName="Maja",LastName="Kovac",OrgType="",OrgId=4},
                new Client{FirstName="Andrej",LastName="Potocnik",OrgType="",OrgId=5},
                new Client{FirstName="Irena",LastName="Mlakar",OrgType="",OrgId=6}*/
            };

            context.Clients.AddRange(clients);
            context.SaveChanges();
            
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

            //add admin user
            var user = new ApplicationUser
            {
                Email = "jhribar8@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ClientId = context.Clients.First().ClientId
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user,"Admin!23");
                user.PasswordHash = hashed;
                context.Users.Add(user);
                
            }
            
            context.SaveChanges();

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


            var sensors = new Sensor[]
            {
                new Sensor{SerialNumber="1",Type="senTemp",Location="Vhod",FirmwareVersion="1"}/*,
                new Sensor{SerialNumber="2",Type="senHOCL",Location="Glavni bazen",FirmwareVersion="1"},
                new Sensor{SerialNumber="66212",Type="senHOCL",Location="Tobogan",FirmwareVersion="1"},
                new Sensor{SerialNumber="08990",Type="senHOCL",Location="Tobogan",FirmwareVersion="1"},
                new Sensor{SerialNumber="41769",Type="senHOCL",Location="Savna",FirmwareVersion="1"},
                new Sensor{SerialNumber="83990",Type="senPH",Location="Glavni bazen",FirmwareVersion="1"},
                new Sensor{SerialNumber="10088",Type="senORP",Location="Glavni bazen",FirmwareVersion="1"}*/
            };

            context.Sensors.AddRange(sensors);
            context.SaveChanges();
            
            var accesses = new SensorAccess[]
            {
                new SensorAccess{ClientId=1,SensorId=context.Sensors.First().SensorId}/*,
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
                new SensorAccess{ClientId=7,SensorId=3141},*/
            };

            context.Accesses.AddRange(accesses);
            context.SaveChanges();
            
        }
    }
}