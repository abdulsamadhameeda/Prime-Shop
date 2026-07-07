
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Domain.Entities;

namespace PrimeShop.Infrastructure.Persistence.Seed
{
    public class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context , RoleManager<IdentityRole> roleManager)
        {
            if (!await context.Lookups.AnyAsync())
            {
                var lookups = new List<Lookup>
                {
                    new Lookup { Id = Guid.NewGuid(), Type = "Role", Value = "Admin" },
                    new Lookup { Id = Guid.NewGuid(), Type = "Role", Value = "Customer" },


                   new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Amman" },
                   new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Zarqa" },
                   new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Irbid" },
                   new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Aqaba" },
                   new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Mafraq" },
                   new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Jerash" },
                   new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Ajloun" },
                   new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Karak" },
                   new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Tafilah" },
                   new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Madaba" },
                   new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Balqa" },
                };

                await context.Lookups.AddRangeAsync(lookups);
                await context.SaveChangesAsync();
            }
            // Seed Roles في AspNetRoles
            if (!await roleManager.RoleExistsAsync("Admin")) 
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }


        }
    }

}
