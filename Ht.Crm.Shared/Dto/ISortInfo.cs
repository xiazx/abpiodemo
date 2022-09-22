using System.Collections.Generic;

namespace Abp.Demo.Shared.Dto
{
    /// <summary>
    /// ≈≈–Ú–≈œ¢
    /// </summary>
    public interface ISortInfo
    {
        /// <summary>
        /// ≈≈–Ú◊÷∂Œ
        /// </summary>
        /// <example>['SortNo desc', 'CreateTime desc']</example>
        IList<string> SortFields { get; set; }
    }
}