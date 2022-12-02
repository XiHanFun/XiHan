#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RegexHelper
// Guid:351b39db-a1a2-4d26-94bb-96a924fba528
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 上午 01:16:10
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text.RegularExpressions;

namespace ZhaiFanhuaBlog.Utils.Verification;

/// <summary>
/// 字符验证帮助类
/// </summary>
public static class RegexHelper
{
    #region 验证输入字符串是否与模式字符串匹配

    /// <summary>
    /// 验证输入字符串是否与模式字符串匹配，匹配返回true
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="pattern">模式字符串</param>
    public static bool IsMatch(string input, string pattern)
    {
        return IsMatch(input, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 验证输入字符串是否与模式字符串匹配，匹配返回true
    /// </summary>
    /// <param name="input">输入的字符串</param>
    /// <param name="pattern">模式字符串</param>
    /// <param name="options">筛选条件</param>
    public static bool IsMatch(string input, string pattern, RegexOptions options)
    {
        return Regex.IsMatch(input, pattern, options);
    }

    #endregion

    #region 是否GUID

    /// <summary>
    /// Guid格式验证（a480500f-a181-4d3d-8ada-461f69eecfdd）
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsGuid(string checkValue)
    {
        string pattern = @"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    #endregion

    #region 是否中国电话

    /// <summary>
    /// 电话号码（正确格式为：xxx-xxxxxxx或xxxx-xxxxxxxx）
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_Tel(string checkValue)
    {
        string pattern = @"^(\d{3,4}-)\d{7,8}$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    #endregion

    #region 是否身份证

    /// <summary>
    /// 验证身份证是否有效
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_People(string checkValue)
    {
        if (checkValue.Length == 18)
        {
            bool check = IsNumber_People_18(checkValue);
            return check;
        }
        else if (checkValue.Length == 15)
        {
            bool check = IsNumber_People_15(checkValue);
            return check;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 身份证号（18位数字）
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_People_18(string checkValue)
    {
        // 数字验证
        if (long.TryParse(checkValue.Remove(17), out long n) == false || n < Math.Pow(10, 16) || long.TryParse(checkValue.Replace('x', '0').Replace('X', '0'), out n) == false)
        {
            return false;
        }
        // 省份验证
        string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (!address.Contains(checkValue.Remove(2), StringComparison.CurrentCulture))
        {
            return false;
        }
        // 生日验证
        string birth = checkValue.Substring(6, 8).Insert(6, "-").Insert(4, "-");
        if (!DateTime.TryParse(birth, out _))
        {
            return false;
        }
        // 校验码验证
        string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
        string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
        char[] Ai = checkValue.Remove(17).ToCharArray();
        int sum = 0;
        for (int i = 0; i < 17; i++)
        {
            sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
        }
        Math.DivRem(sum, 11, out int y);
        if (arrVarifyCode[y] != checkValue.Substring(17, 1).ToLower())
        {
            return false;
        }
        // 符合GB11643-1999标准
        return true;
    }

    /// <summary>
    /// 身份证号（15位数字）
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_People_15(string checkValue)
    {
        // 数字验证
        if (long.TryParse(checkValue, out long n) == false || n < Math.Pow(10, 14))
        {
            return false;
        }
        // 省份验证
        string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (!address.Contains(checkValue.Remove(2), StringComparison.CurrentCulture))
        {
            return false;
        }
        // 生日验证
        string birth = checkValue.Substring(6, 6).Insert(4, "-").Insert(2, "-");
        if (DateTime.TryParse(birth, out _) == false)
        {
            return false;
        }
        // 符合15位身份证标准
        return true;
    }

    #endregion

    #region 是否邮箱

    /// <summary>
    /// Email地址
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsEmail(string checkValue)
    {
        string pattern = @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    #endregion

    #region 是否数字

    /// <summary>
    /// 数字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber(string checkValue)
    {
        string pattern = @"^[0-9]*$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 是不是Int型
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsInt(string source)
    {
        Regex regex = new(@"^(-){0,1}\d+$");
        if (regex.Match(source).Success)
        {
            if ((long.Parse(source) > 0x7fffffffL) || (long.Parse(source) < -2147483648L))
            {
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 整数或者小数
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_IntOrDouble(string checkValue)
    {
        string pattern = @"^[0-9]+\.{0,1}[0-9]{0,2}$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// N位的数字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_Several_N(string checkValue)
    {
        string pattern = @"^\d{n}$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 至少N位的数字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_Several_AtLeast_N(string checkValue)
    {
        string pattern = @"^\d{n,}$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// M至N位的数字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_Several_M_N(string checkValue)
    {
        string pattern = @"^\d{m,n}$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 零和非零开头的数字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_Begin_ZeroOrNotZero(string checkValue)
    {
        string pattern = @"^(0|[1-9] [0-9]*)$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 2位小数的正实数
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_Positive_Real_TwoDouble(string checkValue)
    {
        string pattern = @"^[0-9]+(.[0-9]{2})?$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 有1-3位小数的正实数
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_Positive_Real_OneOrThreeDouble(string checkValue)
    {
        string pattern = @"^[0-9]+(.[0-9]{1,3})?$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 非零的正整数
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_Positive_Int_NotZero(string checkValue)
    {
        string pattern = @"^\+?[1-9][0-9]*$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 非零的负整数
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumber_Negative_Int_NotZero(string checkValue)
    {
        string pattern = @"^\-?[1-9][0-9]*$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    #endregion

    #region 是否字母

    /// <summary>
    /// 字母
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsLetter(string checkValue)
    {
        string pattern = @"^[A-Za-z]+$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 大写字母
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsLetter_Capital(string checkValue)
    {
        string pattern = @"^[A-Z]+$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 小写字母
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsLetter_Lower(string checkValue)
    {
        string pattern = @"^[a-z]+$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    #endregion

    #region 是否数字或英文字母

    /// <summary>
    /// 数字或英文字母
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsNumberOrLetter(string checkValue)
    {
        string pattern = @"^[A-Za-z0-9]+$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    #endregion

    #region 字符串长度限定

    /// <summary>
    /// 看字符串的长度是不是在限定数之间 一个中文为两个字符
    /// </summary>
    /// <param name="source">字符串</param>
    /// <param name="begin">大于等于</param>
    /// <param name="end">小于等于</param>
    /// <returns></returns>
    public static bool IsLengthStr(string source, int begin, int end)
    {
        int length = Regex.Replace(source, @"[^\x00-\xff]", "OK").Length;
        if ((length <= begin) && (length >= end))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 长度为3的字符
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsChar_Three(string checkValue)
    {
        string pattern = @"^.{3}$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    #endregion

    #region 是否邮政编码

    /// <summary>
    /// 邮政编码 6个数字
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsPostCode(string source)
    {
        return Regex.IsMatch(source, @"^\d{6}$", RegexOptions.IgnoreCase);
    }

    #endregion

    #region 是否特殊字符

    /// <summary>
    /// 是否含有=，。：等特殊字符
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsChar_Special(string checkValue)
    {
        string pattern = @"[^%&',;=?$\x22]+";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    #endregion

    #region 是否汉字

    /// <summary>
    /// 包含汉字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsContainChinese(string checkValue)
    {
        string pattern = @"^[\u4e00-\u9fa5]{0,}$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 全部汉字
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsChinese(string checkValue)
    {
        Regex regex = new("[\u4e00-\u9fa5]");
        if (regex.Matches(checkValue).Count == checkValue.Length)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region 是否网址

    /// <summary>
    /// 是否网址
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsURL(string checkValue)
    {
        string pattern = @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    #endregion

    #region 是否日期

    /// <summary>
    /// 验证日期
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsDateTime(string checkValue)
    {
        try
        {
            DateTime time = Convert.ToDateTime(checkValue);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 一年的12个月（正确格式为："01"～"09"和"1"～"12"）
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsMonth(string checkValue)
    {
        string pattern = @"^^(0?[1-9]|1[0-2])$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 一月的31天（正确格式为："01"～"09"和"1"～"31"）
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsDay(string checkValue)
    {
        string pattern = @"^((0?[1-9])|((1|2)[0-9])|30|31)$";
        return Regex.IsMatch(checkValue, pattern, RegexOptions.IgnoreCase);
    }

    #endregion

    #region 是否IP地址

    /// <summary>
    /// 是否IP地址
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsIP(string checkValue)
    {
        return Regex.IsMatch(checkValue, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$", RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 是否IP地址
    /// </summary>
    /// <param name="checkValue"></param>
    /// <returns></returns>
    public static bool IsIp(string checkValue)
    {
        bool result = false;
        try
        {
            string[] checkValuearg = checkValue.Split('.');
            if (string.Empty != checkValue && checkValue.Length < 16 && checkValuearg.Length == 4)
            {
                int intcheckValue;
                for (int i = 0; i < 4; i++)
                {
                    intcheckValue = Convert.ToInt16(checkValuearg[i]);
                    if (intcheckValue > 255)
                    {
                        result = false;
                        return result;
                    }
                }
                result = true;
            }
        }
        catch
        {
            return result;
        }
        return result;
    }

    #endregion
}