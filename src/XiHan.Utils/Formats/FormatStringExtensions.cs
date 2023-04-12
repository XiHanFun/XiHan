#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:FormatStringExtensions
// Guid:55d89543-b256-41ce-ba60-41ef0e0c4c4e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-03 下午 08:15:19
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;

namespace XiHan.Utils.Formats;

/// <summary>
/// 字符串格式化帮助类
/// </summary>
public static class FormatStringExtensions
{
    /// <summary>
    /// 字符串整体替换
    /// </summary>
    /// <param name="content"></param>
    /// <param name="oldStr"></param>
    /// <param name="newStr"></param>
    /// <returns></returns>
    public static string ReplaceStr(this string content, string oldStr, string newStr)
    {
        // 没有替换字符串直接返回源字符串
        if (!content.Contains(oldStr, StringComparison.CurrentCulture)) return content;
        // 有替换字符串开始替换
        StringBuilder strBuffer = new();
        var start = 0;
        var end = 0;
        // 查找替换内容，把它之前和上一个替换内容之后的字符串拼接起来
        while (true)
        {
            start = content.IndexOf(oldStr, start, StringComparison.Ordinal);
            if (start == -1) break;
            strBuffer.Append(content[end..start]);
            strBuffer.Append(newStr);
            start += oldStr.Length;
            end = start;
        }
        // 查找到最后一个位置之后，把剩下的字符串拼接进去
        strBuffer.Append(content[end..]);
        return strBuffer.ToString();
    }
}