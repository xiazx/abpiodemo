using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.Demo.Shared.Utils
{
    /// <summary>
    /// 文件处理帮助类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 判断是否是图片
        /// </summary>
        public static bool IsImgType(string contentType)
        {
            if (contentType == "image/jpeg" || contentType == "image/gif" || contentType == "image/png" || contentType == "image/bmp" || contentType == "application/octet-stream")
                return true;
            else
                return false;
        }

    }
}
