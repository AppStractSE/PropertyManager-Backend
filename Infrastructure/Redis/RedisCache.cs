using Domain.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.Redis
{
    public class RedisCache : IRedisCache
    {
        private readonly TimeSpan _defaultExpiry = TimeSpan.FromMinutes(30);
        private readonly IDatabase _db;


        public RedisCache(IConnectionMultiplexer connectionMultiplexer)
        {
            _db = connectionMultiplexer.GetDatabase();
        }

        public bool Exists(string key)
        {
            return _db.KeyExists(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {

            var value = await _db.StringGetAsync(key);
            return value.HasValue ? JsonConvert.DeserializeObject<T>(value) : default;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            await _db.StringSetAsync(key, JsonConvert.SerializeObject(value), expiry ?? _defaultExpiry);
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }

    }
}
