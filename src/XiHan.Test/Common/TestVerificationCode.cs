#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:TestVerificationCode
// Guid:e13c449a-85b1-42b3-89db-1291bb7d6779
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/3 2:49:30
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Diagnostics;
using XiHan.Utils.Verifications;

namespace XiHan.Test.Common;

/// <summary>
/// 测试生成验证码
/// </summary>
public static class TestVerificationCode
{
    /// <summary>
    /// 生成验证码
    /// </summary>
    public static void RunVerificationCode()
    {
        Stopwatch stopwatch = new();
        while (true)
        {
            stopwatch.Restart();
            Console.WriteLine(VerificationCodeHelper.CodeNumber(20));
            Console.WriteLine(VerificationCodeHelper.CodeLetter(20));
            Console.WriteLine(VerificationCodeHelper.CodeNumberOrLetter(20));
            stopwatch.Stop();
            Console.WriteLine($"用时：{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}