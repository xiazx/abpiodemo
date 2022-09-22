using System;

namespace Abp.Demo.Infra.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class CacheEntityAttribute : Attribute
    {
        public CacheEntityAttribute()
        {
            
        }
    }
}
