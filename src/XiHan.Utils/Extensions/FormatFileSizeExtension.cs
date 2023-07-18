#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:FormatFileSizeExtensions
// Guid:79ce6f67-840d-4504-a5c9-242e14466bd5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-06-03 下午 10:04:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Utils.Extensions;

/// <summary>
/// 文件大小格式化拓展类
/// </summary>
public static class FormatFileSizeExtension
{
    private static readonly string[] _suffixes = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };

    /// <summary>
    /// 格式化文件大小显示为字符串
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string FormatByteToString(this long bytes)
    {
        double last = 1;
        for (var i = 0; i < _suffixes.Length; i++)
        {
            var current = Math.Pow(1024, i + 1);
            var temp = bytes / current;
            if (temp < 1) return (bytes / last).ToString("f3") + _suffixes[i];
            last = current;
        }

        return bytes.ToString();
    }
}