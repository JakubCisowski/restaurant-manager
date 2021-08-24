using EasyCaching.Core;
using Microsoft.Extensions.Options;
using RestaurantManager.Consts.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Core.Cache
{
    public class EasyCacheService : ICacheService
    {
        private readonly IEasyCachingProvider _provider;
        private readonly CacheConfig _cacheConfig;

        public EasyCacheService(IEasyCachingProvider provider,
                                IOptions<CacheConfig> cacheConfig)
        {
            _provider = provider;
            _cacheConfig = cacheConfig.Value;
        }
        public void Clear()
        {
            _provider.Flush();
        }

        public T Get<T>(string key, Func<T> acquire, int? cacheTime = null)
        {
            return _provider.Get(key, acquire, TimeSpan.FromMinutes(cacheTime ?? _cacheConfig.CacheTime))
               .Value;
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, int? cacheTime = null)
        {
            var t = await _provider.GetAsync(key, acquire, TimeSpan.FromMinutes(cacheTime ?? _cacheConfig.CacheTime));
            return t.Value;
        }

        public bool IsSet(string key)
        {
            return _provider.Exists(key);
        }

        public void Remove(string key)
        {
            _provider.Remove(key);
        }

        public void RemoveByPrefix(string prefix)
        {
            _provider.RemoveByPrefix(prefix);
        }

        public void Set(string key, object data, int cacheTime)
        {
            _provider.Set(key, data, TimeSpan.FromMinutes(cacheTime));
        }
    }
}
