using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;

namespace Domain;

public static class Core
{
    public static void InitDomain(this WebApplicationBuilder builder, TypeAdapterConfig config)
    {
        // Setup mapster
        config.Scan(typeof(Core).Assembly);

        //Setup MediatR
        builder.Services.AddMediatR(typeof(Core).Assembly);
    }
}
