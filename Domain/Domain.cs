using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;

namespace Domain;

public static class Domain
{
    public static void InitDomain(this WebApplicationBuilder builder, TypeAdapterConfig config)
    {
        // Setup mapster
        config.Scan(typeof(Domain).Assembly);

        //Setup MediatR
        builder.Services.AddMediatR(typeof(Domain).Assembly);
    }
}
