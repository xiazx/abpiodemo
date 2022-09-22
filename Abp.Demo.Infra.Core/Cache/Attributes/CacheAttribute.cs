using System;

namespace Abp.Demo.Infra.Core
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CacheAttribute : Attribute
    {
        public string Key { get; set; }

        //
        // 摘要:
        //     Gets or sets how long a cache entry can be inactive (e.g. not accessed) before
        //     it will be removed. This will not extend the entry lifetime beyond the absolute
        //     expiration (if set).
        public int? SlidingSecond { get; set; }
        
        public CacheAttribute(string key)
        {
            Key = key;
        }

        public CacheAttribute(string key, int slidingSecond)
        {
            Key = key;
            SlidingSecond = slidingSecond;
        }
    }
}
