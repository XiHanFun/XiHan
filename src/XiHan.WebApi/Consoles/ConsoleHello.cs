#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ConsoleHello
// Guid:5cf676a5-01b1-4135-90d2-976b1f491419
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-10 下午 11:12:04
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Infos;
using XiHan.Utils.Extensions;

namespace XiHan.WebApi.Consoles;

/// <summary>
/// ConsoleHello
/// </summary>
public static class ConsoleHello
{
    /// <summary>
    /// 欢迎使用曦寒
    /// </summary>
    public static void SayHello()
    {
        HelloInfoHelper.Logo.WriteLineHandle();
        HelloInfoHelper.SendWord.WriteLineHandle();
        HelloInfoHelper.Copyright.WriteLineHandle();
        $@"官方文档：{HelloInfoHelper.OfficialDocuments}".WriteLineHandle();
        $@"官方组织：{HelloInfoHelper.OfficialOrganization}".WriteLineHandle();
        $@"源码仓库：{HelloInfoHelper.SourceCodeRepository}".WriteLineHandle();
    }
}