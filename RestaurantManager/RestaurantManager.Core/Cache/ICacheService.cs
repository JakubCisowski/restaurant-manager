using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Core.Cache
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, int? cacheTime = null);
        T Get<T>(string key, Func<T> acquire, int? cacheTime = null);
        void Set(string key, object data, int cacheTime);
        bool IsSet(string key);
        void Remove(string key);
        void RemoveByPrefix(string prefix);
        void Clear();
    }
}
