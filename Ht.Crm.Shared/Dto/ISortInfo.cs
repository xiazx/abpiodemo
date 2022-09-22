using System.Collections.Generic;

namespace Abp.Demo.Shared.Dto
{
    /// <summary>
    /// ������Ϣ
    /// </summary>
    public interface ISortInfo
    {
        /// <summary>
        /// �����ֶ�
        /// </summary>
        /// <example>['SortNo desc', 'CreateTime desc']</example>
        IList<string> SortFields { get; set; }
    }
}