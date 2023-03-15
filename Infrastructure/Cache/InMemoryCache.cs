using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Internal;
using Core.Repository.Interfaces;

namespace Infrastructure.Cache
{
    public class InMemoryCache : ICache
    {
        IMemoryCache _memoryCache;

        public InMemoryCache()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public bool Exists(string key)
        {
            return _memoryCache.TryGetValue(key, out object value);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await Task.Run(() =>
            {
                _memoryCache.TryGetValue(key, out object value);
                return (T)value;
            });
        }

        public async Task RemoveAsync(string key)
        {
            await Task.Run(() => _memoryCache.Remove(key));
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            await Task.Run(() => _memoryCache.Set(key, value, expiry ?? TimeSpan.FromMinutes(5)));
        }
    }
}
