using Abp.Demo.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Abp.Demo.Infra.Core
{
    public static class QueryHelperExtension
    {
        /// <summary>
        /// 查询指定条件的数据
        /// </summary>
        /// <typeparam name="TEntity">查询的实体</typeparam>
        /// <typeparam name="TDto">返回的类型</typeparam>
        /// <param name="source"></param>
        /// <param name="query">查询条件</param>
        /// <param name="cancellationToken"></param>
        public static Task<TDto> FirstOrDefaultAsync<TEntity, TDto>(this IQueryable<TEntity> source, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var data = source.ProjectTo<TDto>();
            return data.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// 查询指定条件的数据
        /// </summary>
        /// <typeparam name="TEntity">查询的实体</typeparam>
        /// <typeparam name="TDto">返回的类型</typeparam>
        /// <param name="source"></param>
        /// <param name="query">查询条件</param>
        /// <param name="cancellationToken"></param>
        public static Task<TDto> FirstOrDefaultAsync<TEntity, TDto>(this IQueryable<TEntity> source,
            IQuery query, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var data = source.Where(query).ProjectTo<TDto>();

            if (query is ISortInfo sort)
            {
                data = data.OrderBy(sort);
            }

            return data.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// 查询指定条件的数据
        /// </summary>
        /// <typeparam name="TEntity">查询的实体</typeparam>
        /// <typeparam name="TDto">返回的类型</typeparam>
        /// <param name="source"></param>
        /// <param name="query">查询条件</param>
        /// <param name="cancellationToken"></param>
        public static Task<TDto> FirstOrDefaultAsync<TEntity, TDto>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> query, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var data = source.Where(query).ProjectTo<TDto>();
            return data.FirstOrDefaultAsync(cancellationToken);
        }


        /// <summary>
        /// 查询指定条件的数据
        /// </summary>
        /// <typeparam name="TEntity">查询的实体</typeparam>
        /// <typeparam name="TDto">返回的类型</typeparam>
        /// <param name="source"></param>
        /// <param name="query">查询条件</param>
        /// <param name="sort">排序</param>
        /// <param name="defaultSort">默认排序</param>
        /// <param name="cancellationToken"></param>
        public static Task<List<TDto>> ToListAsync<TEntity, TDto>(this IQueryable<TEntity> source,
            IQuery query, ISortInfo sort, string defaultSort = null, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var data = source.Where(query).ProjectTo<TDto>().OrderBy(sort, defaultSort);
            return data.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 查询指定条件的数据
        /// </summary>
        /// <typeparam name="TEntity">查询的实体</typeparam>
        /// <typeparam name="TDto">返回的类型</typeparam>
        /// <param name="source"></param>
        /// <param name="query">查询条件</param>
        /// <param name="sort">排序</param>
        /// <param name="defaultSort">默认排序</param>
        /// <param name="cancellationToken"></param>
        public static Task<List<TDto>> ToListAsync<TEntity, TDto>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> query, ISortInfo sort, string defaultSort = null, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var data = source.Where(query).ProjectTo<TDto>().OrderBy(sort, defaultSort);
            return data.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 查询指定条件的数据
        /// </summary>
        /// <typeparam name="TEntity">查询的实体</typeparam>
        /// <typeparam name="TDto">返回的类型</typeparam>
        /// <param name="source"></param>
        /// <param name="sort">排序</param>
        /// <param name="defaultSort">默认排序</param>
        /// <param name="cancellationToken"></param>
        public static Task<List<TDto>> ToListAsync<TEntity, TDto>(this IQueryable<TEntity> source,
            ISortInfo sort, string defaultSort = null, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var data = source.ProjectTo<TDto>().OrderBy(sort, defaultSort);
            return data.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 查询指定条件的数据
        /// </summary>
        /// <typeparam name="TEntity">查询的实体</typeparam>
        /// <typeparam name="TDto">返回的类型</typeparam>
        /// <param name="source"></param>
        /// <param name="query">查询条件</param>
        /// <param name="cancellationToken"></param>
        public static Task<List<TDto>> ToListAsync<TEntity, TDto>(this IQueryable<TEntity> source,
            IQuery query, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var data = source.Where(query).ProjectTo<TDto>();
            return data.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 查询指定条件的数据
        /// </summary>
        /// <typeparam name="TEntity">查询的实体</typeparam>
        /// <typeparam name="TDto">返回的类型</typeparam>
        /// <param name="source"></param>
        /// <param name="query">查询条件</param>
        /// <param name="cancellationToken"></param>
        public static Task<List<TDto>> ToListAsync<TEntity, TDto>(this IQueryable<TEntity> source,
            CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var data = source.ProjectTo<TDto>();
            return data.ToListAsync(cancellationToken);
        }


        /// <summary>
        /// 查询指定条件的数据
        /// </summary>
        /// <typeparam name="TEntity">查询的实体</typeparam>
        /// <typeparam name="TDto">返回的类型</typeparam>
        /// <param name="source"></param>
        /// <param name="query">查询条件</param>
        /// <param name="defaultSort">默认排序</param>
        /// <param name="sortByEntity">是否按实体字段排序</param>
        /// <param name="cancellationToken"></param>
        public static Task<PageResult<TDto>> ToPageResultAsync<TEntity, TDto>(this IQueryable<TEntity> source,
            IPageQuery query, string defaultSort = null, bool sortByEntity = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            return ToPageResultAsync<TEntity, TDto>(source, query, query, defaultSort, sortByEntity, cancellationToken);
        }

        /// <summary>
        /// 查询指定条件的数据
        /// </summary>
        /// <typeparam name="TEntity">查询的实体</typeparam>
        /// <typeparam name="TDto">返回的类型</typeparam>
        /// <param name="source"></param>
        /// <param name="query">查询条件</param>
        /// <param name="page">分页信息</param>
        /// <param name="defaultSort">默认排序</param>
        /// <param name="sortByEntity">是否按实体字段排序</param>
        /// <param name="cancellationToken"></param>
        public static async Task<PageResult<TDto>> ToPageResultAsync<TEntity, TDto>(this IQueryable<TEntity> source,
            IQuery query, IPageSortInfo page, string defaultSort = null, bool sortByEntity = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            page = page ?? new PageSortInfo();
            var pageIndex = Math.Max(1, page.PageIndex);
            var pageSize = Math.Max(1, page.PageSize);

            var result = new PageResult<TDto> { PageIndex = pageIndex, PageSize = pageSize };

            if (sortByEntity)
            {
                var data = source.Where(query);
                result.TotalCount = await data.CountAsync(cancellationToken);
                if (result.TotalCount > 0)
                {
                    var mapData = data.OrderBy(page, defaultSort);
                    result.Data = await mapData.Skip(pageSize * (pageIndex - 1))
                        .Take(pageSize).ProjectTo<TDto>().ToListAsync(cancellationToken);
                }
            }
            else
            {
                var data = source.Where(query).ProjectTo<TDto>();
                result.TotalCount = await data.CountAsync(cancellationToken);
                if (result.TotalCount > 0)
                {
                    var mapData = data.OrderBy(page, defaultSort);
                    result.Data = await mapData.Skip(pageSize * (pageIndex - 1))
                        .Take(pageSize).ToListAsync(cancellationToken);
                }
            }

            return result;
        }

        /// <summary>
        /// 返回分页的数据数据
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source"></param>
        /// <param name="page">分页信息</param>
        /// <param name="defaultSort">默认排序</param>
        /// <param name="cancellationToken"></param>
        public static async Task<PageResult<TEntity>> ToPageResultAsync<TEntity>(this IQueryable<TEntity> source,
            IPageSortInfo page, string defaultSort = null, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            page = page ?? new PageSortInfo();
            var pageIndex = Math.Max(1, page.PageIndex);
            var pageSize = Math.Max(1, page.PageSize);

            var result = new PageResult<TEntity>() { PageIndex = pageIndex, PageSize = pageSize };
            result.TotalCount = await source.CountAsync(cancellationToken);
            if (result.TotalCount > 0)
            {
                var mapData = source.OrderBy(page, defaultSort);
                result.Data = await mapData.Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).ToListAsync(cancellationToken);
            }

            return result;
        }

        /// <summary>
        /// 查询指定条件的数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="query"></param>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source, IQuery query)
            where TEntity : class
        {
            var filter = query?.GetFilter<TEntity>();
            if (filter != null)
                source = source.Where(filter);
            return source;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public static IQueryable<TEntity> PageBy<TEntity>(this IQueryable<TEntity> source, IPageSortInfo page, string defaultSort = null)
        {
            page = page ?? new PageSortInfo();
            var pageIndex = Math.Max(0, page.PageIndex);
            var pageSize = Math.Max(1, page.PageSize);

            return source.OrderBy(page, defaultSort).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public static async Task<PageResult<TDto>> PageAsync<TEntity, TDto>(this IQueryable<TEntity> source, IPageSortInfo page, string defaultSort = null, CancellationToken cancellationToken = default)
        {
            page = page ?? new PageSortInfo();
            var pageIndex = Math.Max(1, page.PageIndex);
            var pageSize = Math.Max(1, page.PageSize);

            var result = new PageResult<TDto>() { PageIndex = pageIndex, PageSize = pageSize };
            var data = source;
            result.TotalCount = await data.CountAsync(cancellationToken);
            if (result.TotalCount > 0)
            {
                var mapData = data.OrderBy(page, defaultSort).ProjectTo<TDto>();
                result.Data = await mapData.Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).ToListAsync(cancellationToken);
            }

            return result;
        }
    }
}
