// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RegexHelper
// Guid:351b39db-a1a2-4d26-94bb-96a924fba528
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 上午 01:16:10
// ----------------------------------------------------------------

using System.Text.RegularExpressions;

namespace ZhaiFanhuaBlog.Utils.Verification;

/// <summary>
/// 字符验证帮助类
/// </summary>
public static class RegexHelper
{
    /// <summary>
    /// Guid格式验证（a480500f-a181-4d3d-8ada-461f69eecfdd）
    /// </summary>
    /// <param name="guid">Guid</param>
    /// <returns></returns>
    public static bool IsGuid(string guid)
    {
        Regex regex = new(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", RegexOptions.IgnoreCase);
        Match result = regex.Match(guid);
        return result.Success;
    }

    /// <summary>
    /// 电话号码（正确格式为：xxx-xxxxxxx或xxxx-xxxxxxxx）
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumber_Tel(string str)
    {
        Regex regex = new(@"^(\d{3.4}-)\d{7,8}$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 身份证号（15位或18位数字）
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumber_People(string str)
    {
        Regex regex = new(@"^\d{15}|\d{18}$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// Email地址
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsEmail(string str)
    {
        Regex regex = new(@"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 数字或英文字母
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumberOrLetter(string str)
    {
        Regex regex = new(@"^[A-Za-z0-9]+$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 整数或者小数
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumber_IntOrDouble(string str)
    {
        Regex regex = new(@"^[0-9]+\.{0,1}[0-9]{0,2}$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumber(string str)
    {
        Regex regex = new(@"^[0-9]*$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// n位的数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumber_Several_n(string str)
    {
        Regex regex = new(@"^\d{n}$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 至少n位的数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumber_Several_AtLeast_n(string str)
    {
        Regex regex = new(@"^\d{n,}$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// m至n位的数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumber_Several_m_n(string str)
    {
        Regex regex = new(@"^\d{m,n}$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 零和非零开头的数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumber_Begin_ZeroOrNotZero(string str)
    {
        Regex regex = new(@"^(0|[1-9] [0-9]*)$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 2位小数的正实数
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumber_Positive_Real_TwoDouble(string str)
    {
        Regex regex = new(@"^[0-9]+(.[0-9]{2})?$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 有1-3位小数的正实数
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumber_Positive_Real_OneOrThreeDouble(string str)
    {
        Regex regex = new(@"^[0-9]+(.[0-9]{1,3})?$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 非零的正整数
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumber_Positive_Int_NotZero(string str)
    {
        Regex regex = new(@"^\+?[1-9][0-9]*$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 非零的负整数
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumber_Negative_Int_NotZero(string str)
    {
        Regex regex = new(@"^\-?[1-9][0-9]*$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 长度为3的字符
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsChar_Three(string str)
    {
        Regex regex = new(@"^.{3}$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 字母
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsLetter(string str)
    {
        Regex regex = new(@"^[A-Za-z]+$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 大写字母
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsLetter_Capital(string str)
    {
        Regex regex = new(@"^[A-Z]+$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 小写字母
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsLetter_Lower(string str)
    {
        Regex regex = new(@"^[a-z]+$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 是否含有=，。：等特殊字符
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsChar_Special(string str)
    {
        Regex regex = new(@"[^%&',;=?$\x22]+");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 汉字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsChinese(string str)
    {
        Regex regex = new(@"^[\u4e00-\u9fa5]{0,}$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// URL
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsURL(string str)
    {
        Regex regex = new(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 一年的12个月（正确格式为："01"～"09"和"1"～"12"）
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsMonth(string str)
    {
        Regex regex = new(@"^^(0?[1-9]|1[0-2])$");
        Match result = regex.Match(str);
        return result.Success;
    }

    /// <summary>
    /// 一月的31天（正确格式为："01"～"09"和"1"～"31"）
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsDay(string str)
    {
        Regex regex = new(@"^((0?[1-9])|((1|2)[0-9])|30|31)$");
        Match result = regex.Match(str);
        return result.Success;
    }
}