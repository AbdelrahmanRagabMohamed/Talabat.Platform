namespace Talabat.Core.Services
{
    public interface IResponseCacheService
    {
        // Cashe Data
        Task CacheResponseAsync(string Cachekey, object Response, TimeSpan ExpireTime);


        // Get Cashed Data
        Task<string?> GetResponseAsync(string Cachekey);
    }
}
