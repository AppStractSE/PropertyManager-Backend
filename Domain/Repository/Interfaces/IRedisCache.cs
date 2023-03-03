namespace Domain.Repository.Interfaces
{
    public interface IRedisCache
    {
        bool Exists(string key);
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task RemoveAsync(string key);
    }
}
