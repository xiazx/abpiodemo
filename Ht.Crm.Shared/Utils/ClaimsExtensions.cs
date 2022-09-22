﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Abp.Demo.Shared.Utils
{
    /// <summary>
    /// 扩展<see cref="ClaimsPrincipal"/>方法
    /// </summary>
    public static class ClaimsExtensions
    {
        /// <summary>
        /// 获取单个值
        /// </summary>
        public static string GetValue(this IEnumerable<Claim> claims, string type)
        {
            return claims.FirstOrDefault(p => p.Type == type)?.Value;
        }
    }
}
