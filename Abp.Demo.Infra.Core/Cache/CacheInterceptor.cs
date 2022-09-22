using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using Volo.Abp.Aspects;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DynamicProxy;
using Abp.Demo.Shared.Utils;

namespace Abp.Demo.Infra.Core
{
    public class CacheInterceptor : AbpInterceptor, ITransientDependency
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CacheInterceptor(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task InterceptAsync(IAbpMethodInvocation invocation)
        {
            using (var serviceScope = _serviceScopeFactory.CreateScope())
            {
                var distributedCache = serviceScope.ServiceProvider.GetRequiredService<IDistributedCache>();
                var cacheOptions = serviceScope.ServiceProvider.GetRequiredService<IOptions<CacheOption>>().Value;

                if (!ShouldIntercept(invocation, cacheOptions))
                {
                    await invocation.ProceedAsync();
                    return;
                }

                var cacheAttr = invocation.Method.GetCustomAttribute<CacheAttribute>();
                if (cacheAttr != null)
                {

                    var str = await distributedCache.GetStringAsync(cacheAttr.Key);
                    if (!string.IsNullOrEmpty(str))
                    {
                        Type returnType = invocation.Method.ReturnType;
                        if (invocation.Method.ReturnType.IsTaskOrTaskOfT() || invocation.Method.ReturnType.IsValueTaskOrValueTaskOfT())
                        {
                            returnType = invocation.Method.ReturnType.GenericTypeArguments.FirstOrDefault();
                        }

                        invocation.ReturnValue = str.FromJson(returnType);
                        return;
                    }

                    await invocation.ProceedAsync();
                    var option = new DistributedCacheEntryOptions();
                    if (cacheAttr.SlidingSecond.HasValue)
                        option.SetSlidingExpiration(new TimeSpan(0, 0, cacheAttr.SlidingSecond.Value));

                    var value = invocation.ReturnValue.ToJson();
                    await distributedCache.SetStringAsync(cacheAttr.Key, value, option);
                }
                var rmcacheAttr = invocation.Method.GetCustomAttribute<RmCacheAttribute>();
                if (rmcacheAttr != null)
                {
                    await invocation.ProceedAsync();
                    await distributedCache.RemoveAsync(rmcacheAttr.Key);
                }
            }
        }

        protected virtual bool ShouldIntercept(IAbpMethodInvocation invocation,
            CacheOption options)
        {
            if (!options.IsEnabled)
            {
                return false;
            }

            if (AbpCrossCuttingConcerns.IsApplied(invocation.TargetObject, "Caching"))
            {
                return false;
            }

            if (invocation.Method.IsDefined(typeof(CacheAttribute), true))
            {
                return true;
            }
            if (invocation.Method.IsDefined(typeof(RmCacheAttribute), true))
            {
                return true;
            }
            return false;
        }
    }
}
