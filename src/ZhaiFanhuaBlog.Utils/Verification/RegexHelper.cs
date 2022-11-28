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

using Microsoft.Extensions.FileSystemGlobbing.Internal;
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
    /// 身份证号（18位数字）
    /// 身份证组成部分
    /// 1、前2位省级行政区域码，第一位是大区编码，全国共8个大区。
    /// 2、第3 - 4位数字为地级行政区编码
    /// 3、第5 - 6位数字为县级行政区编码
    /// 4、第7 - 14位为生日码，就是你的出生日期
    /// 5、第15 - 17位为顺序码，表示在同一地址码所标识的区域范围内，对同年、月、日出生的人员编定的顺序号。其中第十七位奇数分给男性，偶数分给女性。
    /// 6、第18位为校验码，是根据前面十七位数字码，按照ISO 7064:1983.MOD 11 - 2校验码计算出来的检验码。其中X代表计算出来的10。
    /// 身份证最后一位规则
    /// 1、将前面的身份证号码17位数分别乘以不同的系数。从第一位到第十七位的系数分别为：7－9－10－5－8－4－2－1－6－3－7－9－10－5－8－4－2。
    /// 2、将这17位数字和系数相乘的结果相加。
    /// 3、用加出来和除以11，看余数是多少。
    /// 4、余数只可能有0－1－2－3－4－5－6－7－8－9－10这11个数字。其分别对应的最后一位身份证的号码为1－0－X －9－8－7－6－5－4－3－2。（即余数0对应1，余数1对应0，余数2对应X...）
    /// 5、通过上面得知如果余数是3，就会在身份证的第18位数字上出现的是9。如果对应的数字是2，身份证的最后一位号码就是罗马数字X。
    /// 例如：某男性的身份证号码为【53010219200508011X】
    /// 首先我们得出前17位的乘积和【5×7 + 3×9 + 0×10 + 1×5 + 0×8 + 2×4 + 1×2 + 9×1 + 2×6 + 0×3 + 0×7 + 5×9 + 0×10 + 8×5 + 0×8 + 1×4 + 1×2】是189。
    /// 然后用189除以11得出的结果是189÷11 = 17余下2，也就是说其余数是2。
    /// 最后通过对应规则就可以知道余数2对应的检验码是X。所以，可以判定这是一个正确的身份证号码。
    /// </summary>
    /// <param name="cardId"></param>
    /// <returns></returns>
    public static (bool adopt, string message) IsNumber_People(string cardId)
    {
        try
        {
            Regex regex = new(@"^\d{17}(?:\d|X)$");
            // 首先18位格式检查
            if (regex.Match(cardId).Success)
            {
                // 加权数组,用于验证最后一位的校验数字
                int[] arrWeight = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
                // 最后一位计算出来的校验数组，如果不等于这些数字将不正确
                string[] arrIdLastCheck = { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };
                // 校验和
                int sum = 0;
                //通过循环前16位计算出最后一位的数字
                for (int i = 0; i < arrWeight.Length; i++)
                {
                    sum += arrWeight[i] * int.Parse(cardId[i].ToString());
                }
                // 实际校验位的值
                int lastCheck = sum % 11;
                // 出生日期检查
                string birth = cardId.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                if (DateTime.TryParse(birth, out DateTime time))
                {
                    // 校验位检查
                    if (arrIdLastCheck[lastCheck].Equals(cardId[^1].ToString()))
                    {
                        throw new Exception("OK，身份证验证成功");
                    }
                    else
                    {
                        throw new Exception("NG，身份证最后一位校验错误");
                    }
                }
                else
                {
                    throw new Exception("NG，出生日期验证失败");
                }
            }
            else
            {
                throw new Exception("NG，身份证号格式错误");
            }
        }
        catch (Exception ex)
        {
            string[] result = ex.Message.Split("，");
            if (result[0].ToString().Equals("OK"))
            {
                return (true, result[1].ToString());
            }
            else
            {
                return (false, result[1].ToString());
            }
        }
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