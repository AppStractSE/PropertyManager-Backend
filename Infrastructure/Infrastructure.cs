using Azure.Storage.Blobs;
using BlobStorage.Services;
using Core.Repository.Interfaces;
using Core.Services;
using DotCode.SecurityUtils;
using Infrastructure.Cache;
using Infrastructure.Context;
using Infrastructure.EFCore.Context;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Infrastructure
{
    private const string key = "k3h5/4&75kah5sfjkh/as!hjkfh%a8kjf5ks";

    public static void InitInfrastructure(this WebApplicationBuilder builder, bool initBlobStorage = true, bool initDatabase = true)
    {
        builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<IAreaRepository, AreaRepository>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IChoreRepository, ChoreRepository>();
        builder.Services.AddScoped<ICustomerChoreRepository, CustomerChoreRepository>();
        builder.Services.AddScoped<ITeamRepository, TeamRepository>();
        builder.Services.AddScoped<IPeriodicRepository, PeriodicRepository>();
        builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
        builder.Services.AddScoped<IChoreCommentRepository, ChoreCommentRepository>();
        builder.Services.AddScoped<IChoreStatusRepository, ChoreStatusRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IArchivedChoreStatusRepository, ArchivedChoreStatusRepository>();
        builder.Services.AddScoped<ICityRepository, CityRepository>();


        builder.Services.AddSingleton<ICache, InMemoryCache>();
        if (initBlobStorage && initDatabase) builder.Services.AddHostedService<BackgroundWorker>();

        if (initBlobStorage) builder.Services.InitBlobStorage(builder.Configuration);
        if (initDatabase) builder.Services.InitDatabase(builder.Configuration, builder.Environment.EnvironmentName == "Development");
        //builder.Services.ConfigureRedisConnection(builder.Configuration, key); //Not in use right now
    }

    private static void InitBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var encryptedConnectionString = configuration.GetConnectionString("AzureBlobStorage");
        var decryptedConnectionString = Cipher.DecryptString(encryptedConnectionString, key);
        services.AddSingleton(new BlobServiceClient(decryptedConnectionString));
        services.AddSingleton<IBlobService, BlobService>();
    }

    private static void InitDatabase(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        var encryptedConnectionString = configuration.GetConnectionString("databaseConnection");
        var decryptedConnectionString = Cipher.DecryptString(encryptedConnectionString, key);

        services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(decryptedConnectionString));
        services.AddDbContext<PropertyManagerContext>(options => options.UseSqlServer(decryptedConnectionString));

        var useInMemoryDatabase = configuration["ForceUseInMemoryDatabase"] != null ?
                   bool.Parse(configuration["ForceUseInMemoryDatabase"]!) :
                   configuration.GetConnectionString("databaseConnection") == null;
    }
}
