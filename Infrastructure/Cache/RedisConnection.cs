using DotCode.SecurityUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.Cache;

public static class RedisConnection
{
    public static void ConfigureRedisConnection(this IServiceCollection service, IConfiguration configuration, string key)
    {
        var encryptedConnectionString = configuration.GetConnectionString("cacheConnection");
        var decryptedConnectionString = Cipher.DecryptString(encryptedConnectionString, key);
        var muxer = ConnectionMultiplexer.Connect(decryptedConnectionString);

        service.AddSingleton<IConnectionMultiplexer>(muxer);
    }
}
