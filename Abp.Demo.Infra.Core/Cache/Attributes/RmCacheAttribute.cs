using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Demo.Infra.Core
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RmCacheAttribute: Attribute
    {

        public string Key { get; set; }

        public RmCacheAttribute(string key)
        {
            Key = key;
        }
    }
}
