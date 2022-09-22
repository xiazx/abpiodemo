using Abp.Demo.Shared.Utils;
using System;
using System.Linq.Expressions;

namespace Abp.Demo.Shared.Dto
{
    /// <summary>
    /// ��ҳ��ѯ
    /// </summary>
    public class PageQuery : PageSortInfo, IPageQuery
    {
        /// <summary>
        /// ָ����ѯ����
        /// </summary>
        protected LambdaExpression Filter { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public virtual void And<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            if(Filter == null)
                Filter = filter;
            else
                Filter = (Filter as Expression<Func<TEntity, bool>>).And(filter);
        }
        
        /// <summary>
        /// ��ȡ��ѯ����
        /// </summary>
        /// <typeparam name="TEntity">Ҫ��ѯ��ʵ������</typeparam>
        public Expression<Func<TEntity, bool>> GetFilter<TEntity>() where TEntity : class
        {
            And(this.GetQueryExpression<TEntity>());
            return Filter as Expression<Func<TEntity, bool>>;
        }

    }
}