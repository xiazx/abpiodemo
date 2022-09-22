namespace Abp.Demo.Infra.Core
{
    /// <summary>
    /// 缓存配置
    /// </summary>
    public class CacheOption
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }

        public string Configuration { get; set; }
    }
}
