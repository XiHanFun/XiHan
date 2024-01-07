#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:CaptchaTests
// Guid:f260c961-bf46-4658-998c-cb572a9e4d20
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/1/7 22:51:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using XiHan.Utils.Verifications;

namespace XiHan.UnitTest.Utils;

/// <summary>
/// CaptchaTests
/// </summary>
[TestClass]
public class CaptchaTests
{
    [TestMethod]
    public void TestMethod()
    {
        CaptchaHelper.CodeNumber(null, null);
        CaptchaHelper.CodeUpperLetter(null, null);
        CaptchaHelper.CodeLowerLetter(null, null);
        CaptchaHelper.CodeNumberOrLetter(null, null);
    }
}