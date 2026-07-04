
using Microsoft.EntityFrameworkCore;
using PrimeShop.Domain.Entities;

namespace PrimeShop.Infrastructure.Persistence.Seed
{
    public class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!await context.Lookups.AnyAsync())
            {
                var lookups = new List<Lookup>
                {
                    new Lookup { Id = Guid.NewGuid(), Type = "Role", Value = "Admin" },
                    new Lookup { Id = Guid.NewGuid(), Type = "Role", Value = "Customer" },


                    new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Amman" },
                    new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Zarqa" },
                    new Lookup { Id = Guid.NewGuid(), Type = "City", Value = "Irbid" }
                };

                await context.Lookups.AddRangeAsync(lookups);
                await context.SaveChangesAsync();
            }
        }
    }

}
