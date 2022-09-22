
using Abp.Demo.Infra.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EventBus;
using Volo.Abp.Reflection;

namespace AbpIo.Demo.Caches
{
    public class EntityCacheRegistrar
    {
        private readonly IServiceCollection _services;
        public EntityCacheRegistrar(IServiceCollection services)
        {
            _services = services;
        }

        public void AddDefaultEventHandler<TDbContext>() where TDbContext:AbpDbContext<TDbContext>
        {
            foreach (var entityType in GetEntityTypes(typeof(TDbContext)))
            {
                var attr = entityType.GetCustomAttribute<CacheEntityAttribute>();
                if (attr == null)
                    continue;

                var entityHandlerType = typeof(EntityChangeHandler<>).MakeGenericType(entityType);
                _services.AddTransient(entityHandlerType);
            }
        }

        private IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            return
                from property in dbContextType.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>)) &&
                    typeof(IEntity).IsAssignableFrom(property.PropertyType.GenericTypeArguments[0])
                select property.PropertyType.GenericTypeArguments[0];
        }


        public class EntityChangeHandler<TEntity> : ILocalEventHandler<EntityDeletedEventData<TEntity>>,
            ILocalEventHandler<EntityCreatedEventData<TEntity>>,
            ILocalEventHandler<EntityUpdatedEventData<TEntity>> where TEntity : IEntity
        {

            private readonly IEntityCache _entityCache;
            public EntityChangeHandler(IEntityCache entityCache)
            {
                _entityCache = entityCache;
            }

            public async Task HandleEventAsync(EntityDeletedEventData<TEntity> eventData)
            {
                var key = GenerateKey(eventData.Entity.GetKeys());
                await _entityCache.DeleteAsync<TEntity>(key);
            }

            public async Task HandleEventAsync(EntityUpdatedEventData<TEntity> eventData)
            {
                //var key = GenerateKey(eventData.Entity.GetKeys());
                //await _entityCache.SetAsync<TEntity>(key, eventData.Entity);
                await _entityCache.ClearAsync<TEntity>(); 
            }

            public async Task HandleEventAsync(EntityCreatedEventData<TEntity> eventData)
            {
                //var key = GenerateKey(eventData.Entity.GetKeys());
                //await _entityCache.SetAsync<TEntity>(key, eventData.Entity);
                await _entityCache.ClearAsync<TEntity>();
            }

            private string GenerateKey(object[] keys)
            {
                return string.Join("_", keys);
            }
        }
    }

    public static class CacheExtensionHelper
    {
        public static void AddEntityCache<TDbContext>(this IServiceCollection services) where TDbContext : AbpDbContext<TDbContext>
        {
            var configuration = services.GetConfiguration();
            var redisEnabled = configuration["Redis:IsEnabled"];
            if (!redisEnabled.IsNullOrEmpty() && bool.Parse(redisEnabled))
            {
                new EntityCacheRegistrar(services).AddDefaultEventHandler<TDbContext>();
            }
        }
    }
}
