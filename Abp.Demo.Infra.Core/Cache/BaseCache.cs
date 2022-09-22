using Microsoft.Extensions.Options;
using System.Reflection;

namespace Abp.Demo.Infra.Core
{
    public abstract class BaseCache
    {
        protected CacheOption CacheOption { get; }

        public BaseCache(IOptions<CacheOption> cacheOption)
        {
            CacheOption=cacheOption.Value;
        }

        protected virtual string GetKey(string key)
        {
            return CacheOption.Prefix + key;
        }

        protected virtual string GetKey<T>()
        {
            var attr=typeof(T).GetCustomAttribute<CacheNameAttribute>();
            if (attr != null)
                return GetKey(attr.Key);
            return GetKey(typeof(T).Name); 
        }

        protected virtual string GetKey(Type t)
        {
            var attr = t.GetCustomAttribute<CacheNameAttribute>();
            if (attr != null)
                return GetKey(attr.Key);
            return GetKey(t.Name);
        }
    }
}
