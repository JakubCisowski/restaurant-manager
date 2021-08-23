using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RestaurantManager.Consts.Configs;
using System;
using System.Threading.Tasks;

namespace RestaurantManager.Core.Cache
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheConfig _cacheConfig;

        public MemoryCacheService(IMemoryCache memoryCache,
                                  IOptions<CacheConfig> cacheConfig)
        {
            _memoryCache = memoryCache;
            _cacheConfig = cacheConfig.Value;
        }
        public void Clear()
        {
            throw new NotImplementedException();
            //_memoryCache.
        }

        public T Get<T>(string key, Func<T> acquire, int? cacheTime = null)
        {
            return _memoryCache.GetOrCreate(key, x =>
            {
                x.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheTime ?? _cacheConfig.CacheTime);
                return acquire();
            });
        }

        public Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, int? cacheTime = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, int cacheTime = 0)
        {
            throw new NotImplementedException();
        }

        public bool IsSet(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPrefix(string prefix)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object data, int cacheTime)
        {
            if (cacheTime == 0)
            {
                cacheTime = _cacheConfig.CacheTime;
            }
            _memoryCache.Set(key, data, TimeSpan.FromMinutes(cacheTime));
        }
    }
}
