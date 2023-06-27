#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:StringExtensions
// Guid:3630d8a8-77e0-45eb-a1e6-f9a6b5dc26ba
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-03 上午 12:30:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;
using System.Text.RegularExpressions;

namespace XiHan.Utils.Extensions;

/// <summary>
/// StringExtensions
/// </summary>
public static class StringExtension
{
    #region 分割组装

    /// <summary>
    /// 把字符串按照分隔符转换成 List
    /// </summary>
    /// <param name="str">源字符串</param>
    /// <param name="separator">分隔符</param>
    /// <param name="toLower">是否转换为小写</param>
    /// <returns></returns>
    public static List<string> GetStrList(this string str, char separator, bool toLower)
    {
        List<string> list = new();
        var ss = str.Split(separator);
        foreach (var s in ss)
        {
            if (string.IsNullOrEmpty(s) || s == separator.ToString()) continue;
            var strVal = s;
            if (toLower) strVal = s.ToLower();
            list.Add(strVal);
        }

        return list;
    }

    /// <summary>
    /// 分割字符串
    /// </summary>
    /// <param name="str"></param>
    /// <param name="splitter"></param>
    /// <returns></returns>
    public static string[]? GetSplitMulti(this string? str, string splitter)
    {
        string[]? strArray = null;
        if (!string.IsNullOrEmpty(str)) strArray = new Regex(splitter).Split(str);
        return strArray;
    }

    /// <summary>
    /// 把字符串按照, 分割转换为数组
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string[] GetStrArray(this string str)
    {
        return str.Split(new char[] { ',' });
    }

    /// <summary>
    /// 把List按照分隔符组装成string类型
    /// </summary>
    /// <param name="list"></param>
    /// <param name="speater"></param>
    /// <returns></returns>
    public static string GetArrayStr(this List<string> list, string speater)
    {
        StringBuilder sb = new();
        for (var i = 0; i < list.Count; i++)
            if (i == list.Count - 1)
            {
                sb.Append(list[i]);
            }
            else
            {
                sb.Append(list[i]);
                sb.Append(speater);
            }

        return sb.ToString();
    }

    /// <summary>
    /// 得到数组列表以逗号分隔的字符串
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static string GetArrayStr(this List<int> list)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < list.Count; i++)
            if (i == list.Count - 1)
            {
                sb.Append(list[i]);
            }
            else
            {
                sb.Append(list[i]);
                sb.Append(',');
            }

        return sb.ToString();
    }

    /// <summary>
    /// 得到字典以逗号分隔的字符串
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static string GetArrayValueStr(this Dictionary<int, int> list)
    {
        var sb = new StringBuilder();
        foreach (var kvp in list) sb.Append(kvp.Value + ",");
        if (list.Count > 0) return DelLastComma(sb.ToString());

        return "";
    }

    /// <summary>
    /// 把字符串按照指定分隔符装成 List 去除重复
    /// </summary>
    /// <param name="oStr"></param>
    /// <param name="sepeater"></param>
    /// <returns></returns>
    public static List<string> GetSubStringList(this string oStr, char sepeater)
    {
        var ss = oStr.Split(sepeater);
        return ss.Where(s => !string.IsNullOrEmpty(s) && s != sepeater.ToString()).ToList();
    }

    #endregion

    #region 删除结尾字符后的字符

    /// <summary>
    /// 删除最后结尾的一个逗号
    /// </summary>
    public static string DelLastComma(this string str)
    {
        return str[..str.LastIndexOf(",", StringComparison.Ordinal)];
    }

    /// <summary>
    /// 删除最后结尾的指定字符后的字符
    /// </summary>
    public static string DelLastChar(this string str, string strchar)
    {
        return str[..str.LastIndexOf(strchar, StringComparison.Ordinal)];
    }

    #endregion

    #region 半角全角转换

    /// <summary>
    /// 半角转全角的函数(SBC case)
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ToSbc(this string input)
    {
        var c = input.ToCharArray();
        for (var i = 0; i < c.Length; i++)
        {
            if (c[i] == 32)
            {
                c[i] = (char)12288;
                continue;
            }

            if (c[i] < 127)
                c[i] = (char)(c[i] + 65248);
        }

        return new string(c);
    }

    /// <summary>
    /// 全角转半角的函数(SBC case)
    /// </summary>
    /// <param name="input">输入</param>
    /// <returns></returns>
    public static string ToDbc(this string input)
    {
        var c = input.ToCharArray();
        for (var i = 0; i < c.Length; i++)
        {
            if (c[i] == 12288)
            {
                c[i] = (char)32;
                continue;
            }

            if (c[i] > 65280 && c[i] < 65375)
                c[i] = (char)(c[i] - 65248);
        }

        return new string(c);
    }

    #endregion

    #region 转换为纯字符串

    /// <summary>
    ///  将字符串样式转换为纯字符串
    /// </summary>
    /// <param name="strList"></param>
    /// <param name="splitString"></param>
    /// <returns></returns>
    public static string GetCleanStyle(this string? strList, string splitString)
    {
        string? result;
        //如果为空，返回空值
        if (strList == null)
        {
            result = "";
        }
        else
        {
            //返回去掉分隔符
            var newString = strList.Replace(splitString, "");
            result = newString;
        }

        return result;
    }

    #endregion

    #region 转换为新样式

    /// <summary>
    /// 将字符串转换为新样式
    /// </summary>
    /// <param name="strList"></param>
    /// <param name="newStyle"></param>
    /// <param name="splitString"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static string? GetNewStyle(this string? strList, string? newStyle, string splitString, out string error)
    {
        string? returnValue;
        // 如果输入空值，返回空，并给出错误提示
        if (strList == null)
        {
            returnValue = "";
            error = "请输入需要划分格式的字符串";
        }
        else
        {
            //检查传入的字符串长度和样式是否匹配,如果不匹配，则说明使用错误，给出错误信息并返回空值
            var strListLength = strList.Length;
            var newStyleLength = GetCleanStyle(newStyle, splitString).Length;
            if (strListLength != newStyleLength)
            {
                returnValue = "";
                error = "样式格式的长度与输入的字符长度不符，请重新输入";
            }
            else
            {
                // 检查新样式中分隔符的位置
                StringBuilder lengstr = new();
                if (newStyle != null)
                    for (var i = 0; i < newStyle.Length; i++)
                        if (newStyle.Substring(i, 1) == splitString)
                            lengstr.Append(i + ",");

                if (!string.IsNullOrWhiteSpace(lengstr.ToString()))
                {
                    // 将分隔符放在新样式中的位置
                    var str = lengstr.ToString().Split(',');
                    strList = str.Aggregate(strList, (current, bb) => current.Insert(int.Parse(bb), splitString));
                }

                // 给出最后的结果
                returnValue = strList;
                // 因为是正常的输出，没有错误
                error = "";
            }
        }

        return returnValue;
    }

    #endregion

    #region 是否SQL安全字符串

    /// <summary>
    /// 是否SQL安全字符串
    /// </summary>
    /// <param name="str"></param>
    /// <param name="isDel"></param>
    /// <returns></returns>
    public static string SqlSafeString(this string str, bool isDel)
    {
        if (isDel)
        {
            str = str.Replace(@"'", "");
            str = str.Replace(@"""", "");
            return str;
        }

        str = str.Replace(@"'", "&#39;");
        str = str.Replace(@"""", "&#34;");
        return str;
    }

    #endregion

    #region 获取正确的Id，如果不是正整数，返回0

    /// <summary>
    /// 获取正确的Id，如果不是正整数，返回0
    /// </summary>
    /// <param name="value"></param>
    /// <returns>返回正确的整数ID，失败返回0</returns>
    public static int StrToId(this string? value)
    {
        if (!IsNumberId(value)) return 0;
        return value != null ? int.Parse(value) : 0;
    }

    #endregion

    #region 检查验证

    /// <summary>
    /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证(0除外)
    /// </summary>
    /// <param name="value">需验证的字符串。。</param>
    /// <returns>是否合法的bool值。</returns>
    public static bool IsNumberId(this string? value)
    {
        return IsValidateStr("^[1-9]*[0-9]*$", value);
    }

    /// <summary>
    /// 验证一个字符串是否符合指定的正则表达式
    /// </summary>
    /// <param name="express"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsValidateStr(this string express, string? value)
    {
        if (value == null) return false;
        var myRegex = new Regex(express);
        return value.Length != 0 && myRegex.IsMatch(value);
    }

    #endregion

    #region 得到字符串长度，一个汉字长度为2

    /// <summary>
    /// 得到字符串长度，一个汉字长度为2
    /// </summary>
    /// <param name="inputString">参数字符串</param>
    /// <returns></returns>
    public static int StrLength(this string inputString)
    {
        ASCIIEncoding ascii = new();
        var tempLen = 0;
        var s = ascii.GetBytes(inputString);
        foreach (var t in s)
            if (t == 63)
                tempLen += 2;
            else
                tempLen += 1;
        return tempLen;
    }

    #endregion

    #region 截取指定长度字符串

    /// <summary>
    /// 截取指定长度字符串
    /// </summary>
    /// <param name="inputString">要处理的字符串</param>
    /// <param name="len">指定长度</param>
    /// <returns>返回处理后的字符串</returns>
    public static string ClipString(this string inputString, int len)
    {
        var isShowFix = false;
        if (len > 0 && len % 2 == 1)
        {
            isShowFix = true;
            len--;
        }

        ASCIIEncoding ascii = new();
        var tempLen = 0;
        StringBuilder tempString = new();
        var s = ascii.GetBytes(inputString);
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == 63)
                tempLen += 2;
            else
                tempLen += 1;

            try
            {
                tempString.Append(inputString.AsSpan(i, 1));
            }
            catch
            {
                break;
            }

            if (tempLen > len)
                break;
        }

        var mybyte = Encoding.Default.GetBytes(inputString);
        if (isShowFix && mybyte.Length > len)
            tempString.Append('…');
        return tempString.ToString();
    }

    #endregion

    #region HTML转行成TEXT

    /// <summary>
    /// HTML转行成TEXT
    /// </summary>
    /// <param name="strHtml"></param>
    /// <returns></returns>
    public static string HtmlToTxt(this string strHtml)
    {
        string[] aryReg =
        {
            @"<script[^>]*?>.*?</script>",
            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
            @"([\r\n])[\s]+",
            @"&(quot|#34);",
            @"&(amp|#38);",
            @"&(lt|#60);",
            @"&(gt|#62);",
            @"&(nbsp|#160);",
            @"&(iexcl|#161);",
            @"&(cent|#162);",
            @"&(pound|#163);",
            @"&(copy|#169);",
            @"&#(\d+);",
            @"-->",
            @"<!--.*\n"
        };

        var newReg = aryReg[0];
        var strOutput = aryReg.Select(t => new Regex(t, RegexOptions.IgnoreCase))
            .Aggregate(strHtml, (current, regex) => regex.Replace(current, string.Empty));

        var replace = strOutput.Replace("<", "");
        var s = strOutput.Replace(">", "");
        var replace1 = strOutput.Replace("\r\n", "");

        return strOutput;
    }

    #endregion

    #region 首字母处理

    /// <summary>
    /// 首字母大写
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string FirstToUpper(this string value)
    {
        return value[..1].ToUpper() + value[1..];
    }

    /// <summary>
    /// 首字母小写
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string FirstToLower(this string value)
    {
        return value[..1].ToLower() + value[1..];
    }

    #endregion
}