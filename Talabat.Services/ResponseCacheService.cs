using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Services;

namespace Talabat.Services
{
    public class ResponseCacheService : IResponseCacheService
    {

        private readonly IDatabase _database;
        public ResponseCacheService(IConnectionMultiplexer Redis)
        {
            _database = Redis.GetDatabase();
        }

        // Cashe Data
        public async Task CacheResponseAsync(string Cachekey, object Response, TimeSpan ExpireTime)
        {
            if (Response is null) return;

            var Options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var SerializedResponse = JsonSerializer.Serialize(Response, Options);
            await _database.StringSetAsync(Cachekey, SerializedResponse, ExpireTime);

        }

        // Get Cashed Data
        public async Task<string?> GetResponseAsync(string Cachekey)
        {
            var CachedResponse = await _database.StringGetAsync(Cachekey);
            if (CachedResponse.IsNullOrEmpty) return null;

            return CachedResponse;
        }

    }
}
