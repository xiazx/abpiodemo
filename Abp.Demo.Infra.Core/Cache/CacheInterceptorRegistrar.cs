using Volo.Abp.DependencyInjection;
using Volo.Abp.DynamicProxy;

namespace Abp.Demo.Infra.Core
{
    public class CacheInterceptorRegistrar
    {
        public static void RegisterIfNeeded(IOnServiceRegistredContext context)
        {
            if (ShouldIntercept(context.ImplementationType))
            {
                var bl=context.Interceptors.TryAdd<CacheInterceptor>();
            }
        }

        private static bool ShouldIntercept(Type type)
        {
            if (DynamicProxyIgnoreTypes.Contains(type))
            {
                return false;
            }

            //if (ShouldCacheTypeByDefaultOrNull(type) == true)
            //{
            //    return true;
            //}

            if (type.GetMethods().Any(m => m.IsDefined(typeof(CacheAttribute), true)))
            {
                return true;
            }
            if (type.GetMethods().Any(m => m.IsDefined(typeof(RmCacheAttribute), true)))
            {
                return true;
            }
            return false;
        }

        ////TODO: Move to a better place
        //public static bool? ShouldCacheTypeByDefaultOrNull(Type type)
        //{
        //    //TODO: In an inheritance chain, it would be better to check the attributes on the top class first.

        //    if (type.IsDefined(typeof(CacheAttribute), true))
        //    {
        //        return true;
        //    }

        //    if (type.IsDefined(typeof(RmCacheAttribute), true))
        //    {
        //        return true;
        //    }

        //    return null;
        //}
    }
}
