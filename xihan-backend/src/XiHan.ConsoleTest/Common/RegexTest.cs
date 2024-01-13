#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:RegexTest
// Guid:b5401227-996f-45d0-b2b1-2c84742f344c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-29 上午 03:26:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.Verifications;

namespace XiHan.ConsoleTest.Common;

/// <summary>
/// RegexTest
/// </summary>
public class RegexTest
{
    // 身份证号
    public static void TestCardId()
    {
        Console.WriteLine("输入身份证号码");
        var cardId = Console.ReadLine();
        if (cardId != null)
        {
            var result = RegexHelper.IsNumberPeople(cardId);
            Console.WriteLine(result);
        }
        else
        {
            Console.WriteLine("输入错误");
        }
    }
}