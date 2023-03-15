namespace Core.Repository.Interfaces
{
    public interface ICache
    {
        bool Exists(string key);
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task RemoveAsync(string key);
    }
}
