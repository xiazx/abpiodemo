﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Abp.Demo.Shared.Dto
{
    public class JsonExtendResult : Result
    {
        /// <summary>
        /// 扩展信息
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, object> Extend { get; set; } = new Dictionary<string, object>();
    }
}