using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Infrastructure.Identity;
using PrimeShop.Infrastructure.Persistence;
using PrimeShop.Infrastructure.Persistence.Repositories;
using PrimeShop.Infrastructure.Services;
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

            //DAPPER
            services.AddScoped<IDbConnection>(ServiceProvider =>
            {
                var configuration = ServiceProvider.GetRequiredService<IConfiguration>();

                var connectionString = configuration.GetConnectionString("AppContext");

                return new SqlConnection(connectionString);
            });



            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();




            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<JwtTokenService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISqlQueryService, SqlQueryService>();


            return services;
        }
    }
}
