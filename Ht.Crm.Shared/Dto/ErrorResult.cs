﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Abp.Demo.Shared.Dto
{
    /// <summary>
    /// 异常返回结果
    /// </summary>
    public class ErrorResult : Result
    {
        /// <inheritdoc />
        public ErrorResult() : base(ResultCode.Fail)
        {
            Error = new Dictionary<string, object>();
        }

        /// <inheritdoc />
        public ErrorResult(IDictionary<string, object> error, ResultCode code, string message = null)
        {
            Code = code;
            Message = message ?? JsonConvert.SerializeObject(error);
            Error = error;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public IDictionary<string, object> Error { get; set; }
    }
}
