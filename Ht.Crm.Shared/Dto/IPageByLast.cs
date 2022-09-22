﻿namespace Abp.Demo.Shared.Dto
{
    /// <summary>
    /// 增量分页
    /// </summary>
    public interface IPageByLast
    {
        /// <summary>
        /// 增量Id
        /// </summary>
        int? LastId { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        int PageSize { get; set; }
    }
}