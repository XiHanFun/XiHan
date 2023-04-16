#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:UnicodeEncodeHelper
// Guid:434d5241-1747-45af-9663-28a810e3d478
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-15 上午 11:22:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;

namespace XiHan.Utils.Encodes;

/// <summary>
/// Unicode编码帮助类
/// </summary>
public static partial class UnicodeEncodeHelper
{
    /// <summary>
    /// 将字符串转为 Unicode 编码
    /// </summary>
    /// <param name="data">待转码的字符串</param>
    /// <returns>转码后的字符串</returns>
    public static string ToUnicode(this string data)
    {
        StringBuilder sb = new();
        for (int i = 0; i < data.Length; i++)
        {
            sb.AppendFormat(@"\u{0:x4}", (int)data[i]);
        }
        return sb.ToString();
    }

    /// <summary>
    /// 将 Unicode 编码转换为原始的字符串
    /// </summary>
    /// <param name="data">待解码的字符串</param>
    /// <returns>解码后的字符串</returns>
    public static string FromUnicode(this string data)
    {
        return UnicodeRegex().Replace(data, match => ((char)int.Parse(match.Groups[1].Value, System.Globalization.NumberStyles.HexNumber)).ToString());
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"\\u([0-9A-Za-z]{4})")]
    private static partial System.Text.RegularExpressions.Regex UnicodeRegex();
}