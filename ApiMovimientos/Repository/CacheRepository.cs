using ApiMovimientos.Config;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiMovimientos.Repository
{
    public class CacheRepository: ICacheRepository
    {
        private readonly IDistributedCache _cache;

        public CacheRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            try
            {
                var cachedValue = await _cache.GetStringAsync(key);
                if (cachedValue != null)
                {
                    throw new Exception("error 1");
                    //return JsonSerializer.Deserialize<T>(cachedValue);
                }
                return default(T);

            }
            catch (Exception ex)
            {
                Log.Fatal(ex,"error cache 2");
                throw;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            var serializedValue = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, serializedValue, options);
        }
    }
}
