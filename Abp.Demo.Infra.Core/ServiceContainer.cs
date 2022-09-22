using System;

namespace Abp.Demo.Infra.Core
{
    public class ServiceContainer
    {

        public static IServiceProvider ServiceProvider { get; private set; }


        internal static void Set(IServiceProvider provider)
        {
            ServiceProvider = provider;
        }
    }
}
