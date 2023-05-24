#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:VerificationCodeHelper
// Guid:b18b6086-49ff-45f2-abb9-86a7a8505db4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-18 上午 02:20:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;

namespace XiHan.Utils.Verifications;

/// <summary>
/// 验证码帮助类
/// </summary>
public static class VerificationCodeHelper
{
    /// <summary>
    /// 随机数字
    /// </summary>
    /// <param name="length">生成长度 默认6个字符</param>
    /// <param name="source">随机数字字符源</param>
    /// <returns></returns>
    public static string CodeNumber(int length = 6, string source = "0123456789")
    {
        return RandomTo(length, source);
    }

    /// <summary>
    /// 随机字母
    /// </summary>
    /// <param name="length">生成长度 默认6个字符</param>
    /// <param name="source">随机字母字符源</param>
    /// <returns></returns>
    public static string CodeLetter(int length = 6, string source = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")
    {
        return RandomTo(length, source);
    }

    /// <summary>
    /// 随机字母或数字
    /// </summary>
    /// <param name="length">生成长度 默认6个字符</param>
    /// <param name="source">随机字母或数字字符源</param>
    /// <returns></returns>
    public static string CodeNumberOrLetter(int length = 6, string source = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")
    {
        return RandomTo(length, source);
    }

    /// <summary>
    /// 根据随机字符源生成随机字符
    /// </summary>
    /// <param name="length">生成长度</param>
    /// <param name="source">自定义随机的字符源</param>
    /// <returns></returns>
    public static string RandomTo(int length, string source)
    {
        if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));
        if (string.IsNullOrEmpty(source)) throw new ArgumentNullException(nameof(source));

        StringBuilder result = new();
        Random random = new(~unchecked((int)DateTime.Now.Ticks));
        for (var i = 0; i < length; i++)
        {
            Task.Delay(3);
            result.Append(source[random.Next(0, source.Length)]);
        }
        return result.ToString();
    }
}