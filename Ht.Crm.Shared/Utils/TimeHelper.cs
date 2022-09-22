using System;

namespace Abp.Demo.Shared.Utils
{
    public static class TimeHelper
    {
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentTimeStamp()
        {
            return ConvertToTimeStamp(DateTime.UtcNow).ToString();
        }

        /// <summary>
        /// 日期转换为时间戳（时间戳单位毫秒）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp(DateTime time)
        {
            TimeSpan ts = time - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }

        /// <summary>
        /// 时间戳转换为日期（时间戳单位秒）
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(long timeStamp)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddMilliseconds(timeStamp);
        }


    }
}
