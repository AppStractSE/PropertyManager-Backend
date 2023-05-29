using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class Core
{
    public static void InitCore(this WebApplicationBuilder builder, TypeAdapterConfig config)
    {
        //Setup MediatR
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(Core).Assembly);
        });

        // Map Scan
        config.Scan(typeof(Core).Assembly);
    }
}
