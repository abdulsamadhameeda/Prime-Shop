using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PrimeShop.Application.Common.Behaviors;
using PrimeShop.Application.Common.Interfaces;


namespace PrimeShop.Application
{
    public  static class DependencyInjection
    {


        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);



            return services;
        }

    }
}
