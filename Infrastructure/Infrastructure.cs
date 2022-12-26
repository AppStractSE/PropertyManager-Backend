using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Infrastructure
{
    public static void InitInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IChoreRepository, ChoreRepository>();

        builder.Services.InitDatabase(builder.Configuration, builder.Environment.EnvironmentName == "Development");
    }

    private static void InitDatabase(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        var useInMemoryDatabase = true;
        if (configuration.GetConnectionString("defaultDatabase") != null)
        {
            useInMemoryDatabase = !useInMemoryDatabase;
        }
    
        if (useInMemoryDatabase)
        {
            services.AddDbContext<PropertyManagerContext>(c => c.UseInMemoryDatabase("Database"));
        }
        else
        {
            services.AddDbContext<PropertyManagerContext>(c => c.UseSqlServer(configuration.GetConnectionString("defaultDatabase")));
        }
    }
}