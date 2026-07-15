using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Infrastructure;
using PrimeShop.Infrastructure.Identity;
using PrimeShop.Infrastructure.Persistence;
using PrimeShop.Infrastructure.Persistence.Seed;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);   

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); ;
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppContext")));



builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();


builder.Services.AddFluentValidationAutoValidation();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "PrimeShop API V1");
    });


}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    await DbSeeder.SeedAsync(context, roleManager , userManager);
}

app.UseHttpsRedirection();  

app.UseAuthorization();

app.MapControllers();

app.Run();
