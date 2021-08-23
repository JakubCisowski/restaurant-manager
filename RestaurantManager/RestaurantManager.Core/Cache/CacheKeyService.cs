using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Core.Cache
{

    public interface ICacheKeyService
    {
        string GetCacheKey(string prefix, string methodName, params object[] args);
    }
    public class CacheKeyService : ICacheKeyService
    {
        public string GetCacheKey(string prefix, string methodName, params object[] args)
        {
            if (!args.Any())
            {
                args = new[] { "all" };
            }
            var key = string.Join("-", prefix, methodName);

            var argsKeys = string.Join("-", args);
            key = string.Join("-", key, argsKeys);

            return key;
        }
    }
}
