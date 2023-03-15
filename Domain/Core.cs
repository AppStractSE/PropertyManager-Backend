using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;

namespace Core;

public static class Core
{
    public static void InitDomain(this WebApplicationBuilder builder, TypeAdapterConfig config)
    {
        //Setup MediatR
        builder.Services.AddMediatR(typeof(Core).Assembly);

        // Map Scan
        config.Scan(typeof(Core).Assembly);
    }
}
