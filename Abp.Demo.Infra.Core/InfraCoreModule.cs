using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Modularity;

namespace Abp.Demo.Infra.Core
{
    //[DependsOn(typeof(AbpAutoMapperModule))]
    public class InfraCoreModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var redisEnabled = configuration["Redis:IsEnabled"];
            if (!redisEnabled.IsNullOrEmpty() && bool.Parse(redisEnabled))
            {
                var connection = new CSRedisClient(configuration["Redis:Configuration"]);
                RedisHelper.Initialization(connection);
                context.Services.Replace(ServiceDescriptor.Singleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance)));
                context.Services.Replace(ServiceDescriptor.Singleton<IEntityCache, EntityCache>());
                context.Services.OnRegistred(CacheInterceptorRegistrar.RegisterIfNeeded);
            }
        }

        /// <inheritdoc />
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            ServiceContainer.Set(context.ServiceProvider.GetRequiredService<ObjectAccessor<IServiceProvider>>().Value);
        }

    }
}
