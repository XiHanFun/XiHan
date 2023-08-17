#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HelloController
// Guid:1ba84471-fadd-4d4c-94d6-04942cf3b4a1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/21 4:53:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Infos;
using XiHan.Infrastructures.Responses;
using XiHan.WebHost.Controllers.Bases;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebHost.Controllers.Common;

/// <summary>
/// 快速开始
/// </summary>
[AllowAnonymous]
[ApiGroup(ApiGroupNameEnum.Common)]
public class HelloController : BaseApiController
{
    /// <summary>
    /// 欢迎使用曦寒
    /// </summary>
    /// <returns></returns>
    [HttpGet("SayHello")]
    [AppLog(Module = "快速开始", BusinessType = BusinessTypeEnum.Get)]
    public ApiResult SayHello()
    {
        return ApiResult.Success(new
        {
            Hello = "欢迎使用曦寒，一款新型全场景应用软件，基于 DotNet 和 Vue 构建。",
            ProjectInfoHelper.SendWord,
            ProjectInfoHelper.Copyright,
            ProjectInfoHelper.OfficialDocuments,
            ProjectInfoHelper.OfficialOrganization,
            ProjectInfoHelper.SourceCodeRepository,
        });
    }
}