#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysServerController
// Guid:5e620d38-840e-435a-a1dc-aa0460a20fa3
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-20 下午 12:08:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Services.Syses.Servers;
using XiHan.WebApi.Controllers.Bases;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebApi.Controllers.ManageOrDisplay.Syses;

/// <summary>
/// 系统服务器监控管理
/// </summary>
[ApiGroup(ApiGroupNames.Manage)]
public class SysServerController : BaseApiController
{
    private readonly ISysServerService _sysServerService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysServerService"></param>
    public SysServerController(ISysServerService sysServerService)
    {
        _sysServerService = sysServerService;
    }

    /// <summary>
    /// 获取服务器信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("ServerInfo")]
    [AppLog(Module = "系统服务器监控", BusinessType = BusinessTypeEnum.Get)]
    public CustomResult GetServerInfo()
    {
        var serverInfo = _sysServerService.GetServerInfo();
        return CustomResult.Success(serverInfo);
    }
}