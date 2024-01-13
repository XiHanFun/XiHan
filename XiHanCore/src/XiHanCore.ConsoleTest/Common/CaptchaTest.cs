#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:CaptchaTest
// Guid:e13c449a-85b1-42b3-89db-1291bb7d6779
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/3 2:49:30
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Diagnostics;
using XiHanCore.Utils.Verifications;

namespace XiHanCore.ConsoleTest.Common;

/// <summary>
/// 验证码测试
/// </summary>
public static class CaptchaTest
{
    /// <summary>
    /// 生成验证码
    /// </summary>
    public static void Run()
    {
        Stopwatch stopwatch = new();
        while (true)
        {
            stopwatch.Restart();
            Console.WriteLine(CaptchaHelper.CodeNumber(null, null));
            Console.WriteLine(CaptchaHelper.CodeUpperLetter(null, null));
            Console.WriteLine(CaptchaHelper.CodeLowerLetter(null, null));
            Console.WriteLine(CaptchaHelper.CodeNumberOrLetter(null, null));
            stopwatch.Stop();
            Console.WriteLine($"用时：{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}