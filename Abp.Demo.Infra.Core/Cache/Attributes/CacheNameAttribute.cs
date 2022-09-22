using System;

namespace Abp.Demo.Infra.Core
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class CacheNameAttribute : Attribute
    {
        public string Key { get; set; }

        public CacheNameAttribute(string key)
        {
            Key = key;
        }
    }
}
