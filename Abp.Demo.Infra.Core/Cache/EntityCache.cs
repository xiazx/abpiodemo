
using Microsoft.Extensions.Options;
namespace Abp.Demo.Infra.Core
{

    public class EntityCache : BaseCache, IEntityCache
    {
        public EntityCache(IOptions<CacheOption> cacheOption) : base(cacheOption)
        {
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<bool> SetAsync<TEntity>(string field, object value)
        {
            var fullKey = GetKey<TEntity>();
            return RedisHelper.HSetAsync(fullKey, field, value);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<bool> SetAsync(Type entity, string field, object value)
        {
            var fullKey = GetKey(entity);
            return RedisHelper.HSetAsync(fullKey, field, value);
        }

        /// <summary>
        /// 批量添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyVaules"></param>
        /// <returns></returns>
        public Task<bool> MultSetAsync<TEntity>(params object[] keyVaules)
        {
            var fullKey = GetKey<TEntity>();
            return RedisHelper.HMSetAsync(fullKey, keyVaules);
        }


        /// <summary>
        /// 批量添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyVaules"></param>
        /// <returns></returns>
        public Task<bool> MultSetAsync(Type entity, params object[] keyVaules)
        {
            var fullKey = GetKey(entity);
            return RedisHelper.HMSetAsync(fullKey, keyVaules);
        }

        /// <summary>
        /// 获取实体某条记录缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>

        public Task<TEntity> GetAsync<TEntity>(string field)
        {
            var fullKey = GetKey<TEntity>();
            return RedisHelper.HGetAsync<TEntity>(fullKey, field);
        }

        /// <summary>
        /// 获取实体多条记录缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>

        public Task<TEntity[]> GetListAsync<TEntity>(params string[] fields)
        {
            var fullKey = GetKey<TEntity>();
            return RedisHelper.HMGetAsync<TEntity>(fullKey, fields);
        }
        

        /// <summary>
        /// 获取实体缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public Task<TEntity[]> GetAllAsync<TEntity>()
        {
            var fullKey = GetKey<TEntity>();
            return RedisHelper.HValsAsync<TEntity>(fullKey);
        }

        /// <summary>
        /// 删除实体某条记录缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public Task<long> DeleteAsync<TEntity>(params string[] ids)
        {
            var fullKey = GetKey<TEntity>();
            return RedisHelper.HDelAsync(fullKey, ids);
        }

        /// <summary>
        /// 删除实体某条记录缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public Task<long> DeleteAsync(Type entity,params string[] ids)
        {
            var fullKey = GetKey(entity);
            return RedisHelper.HDelAsync(fullKey, ids);
        }

        /// <summary>
        /// 清空实体缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public Task<long> ClearAsync<TEntity>()
        {
            var fullKey = GetKey<TEntity>();
            return RedisHelper.DelAsync(fullKey);
        }
    }
}
