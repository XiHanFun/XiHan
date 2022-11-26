#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:HtmlFormatHelper
// Guid:92253fa2-2ea7-4fbc-b803-fd48f337b515
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-07 上午 03:10:39
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace ZhaiFanhuaBlog.Utils.Formats;

/// <summary>
/// HtmlFormatHelper
/// </summary>
public static partial class HtmlFormatHelper
{
    /// <summary>
    /// 去除富文本中的HTML标签
    /// </summary>
    /// <param name="html"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string ReplaceHtmlTag(string html, int length = 0)
    {
        string strText = MyRegex().Replace(html, "");
        strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");
        if (length > 0 && strText.Length > length)
        {
            return strText[..length];
        }
        return strText;
    }

    [System.Text.RegularExpressions.GeneratedRegex("<[^>]+>")]
    private static partial System.Text.RegularExpressions.Regex MyRegex();
}