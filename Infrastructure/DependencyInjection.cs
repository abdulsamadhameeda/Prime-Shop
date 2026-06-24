using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Infrastructure.Identity;
using PrimeShop.Infrastructure.Persistence;
using PrimeShop.Infrastructure.Persistence.Repositories;

using System.Data;
using System.Text;

namespace PrimeShop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            services.AddScoped<IDbConnection>(ServiceProvider =>
            {
                var configuration = ServiceProvider.GetRequiredService<IConfiguration>();

                var connectionString = configuration.GetConnectionString("AppContext");

                return new SqlConnection(connectionString);
            });



            services.AddIdentityCore<ApplicationUser>()
      .AddRoles<IdentityRole>()
      .AddEntityFrameworkStores<ApplicationDbContext>();
     



            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<JwtTokenService>();


            return services;
        }
    }
}
