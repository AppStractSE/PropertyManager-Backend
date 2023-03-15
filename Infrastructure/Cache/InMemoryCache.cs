using Core.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Internal;

namespace Infrastructure.Cache
{
    public class InMemoryCache : ICache
    {
        IMemoryCache _memoryCache;

        public InMemoryCache()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions()
            {
                SizeLimit = 100,
                ExpirationScanFrequency = TimeSpan.FromMinutes(30),
                Clock = new SystemClock()
            });
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
            await Task.Run(() => _memoryCache.Set(key, value, expiry ?? TimeSpan.FromMinutes(30)));
        }
    }
}
