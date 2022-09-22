using Volo.Abp.DependencyInjection;

namespace Abp.Demo.Infra.Core
{
    public interface IEntityCache
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<bool> SetAsync<TEntity>(string field, object value);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<bool> SetAsync(Type entity, string field, object value);

        /// <summary>
        /// 批量添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyVaules"></param>
        /// <returns></returns>
        public Task<bool> MultSetAsync<TEntity>(params object[] keyVaules);


        /// <summary>
        /// 批量添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyVaules"></param>
        /// <returns></returns>
        public Task<bool> MultSetAsync(Type entity, params object[] keyVaules);

        /// <summary>
        /// 获取实体某条记录缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>

        public Task<TEntity> GetAsync<TEntity>(string field);


        /// <summary>
        /// 获取实体多条记录缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="fields"></param>
        /// <returns></returns>
        public Task<TEntity[]> GetListAsync<TEntity>(params string[] fields);

        /// <summary>
        /// 获取实体缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public Task<TEntity[]> GetAllAsync<TEntity>();
        /// <summary>
        /// 删除实体某条记录缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public Task<long> DeleteAsync<TEntity>(params string[] ids);

        /// <summary>
        /// 删除实体某条记录缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public Task<long> DeleteAsync(Type entity, params string[] ids);

        /// <summary>
        /// 清空实体缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public Task<long> ClearAsync<TEntity>();
    }


    public class NullEntityCache : IEntityCache, ISingletonDependency
    {
        public Task<long> ClearAsync<TEntity>()
        {
            return Task.FromResult<long>(0);
        }

        public Task<long> DeleteAsync<TEntity>(params string[] ids)
        {
            return Task.FromResult<long>(0);
        }

        public Task<long> DeleteAsync(Type entity, params string[] ids)
        {
            return Task.FromResult<long>(0);
        }

        public Task<TEntity[]> GetAllAsync<TEntity>()
        {
            return Task.FromResult<TEntity[]>(null);
        }

        public Task<TEntity> GetAsync<TEntity>(string field)
        {
             return Task.FromResult<TEntity>(default);
        }

        public Task<TEntity[]> GetListAsync<TEntity>(params string[] fields)
        {
            return Task.FromResult<TEntity[]>(null);
        }

        public Task<bool> MultSetAsync<TEntity>(params object[] keyVaules)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> MultSetAsync(Type entity, params object[] keyVaules)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> SetAsync<TEntity>(string field, object value)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<bool> SetAsync(Type entity, string field, object value)
        {
            return Task.FromResult<bool>(false);
        }
    }
}
