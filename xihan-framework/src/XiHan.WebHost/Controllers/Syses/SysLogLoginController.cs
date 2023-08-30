#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogLoginController
// Guid:a6b72a71-814c-43ca-b83c-3313cf432b83
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-24 下午 04:45:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Logging;
using XiHan.Services.Syses.Logging.Dtos;
using XiHan.WebCore.Common.Swagger;
using XiHan.WebHost.Controllers.Bases;

namespace XiHan.WebHost.Controllers.Syses;

/// <summary>
/// 系统登录日志管理
/// </summary>
[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysLogLoginController : BaseApiController
{
    private readonly ISysLogLoginService _sysLogLoginService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysLogLoginService"></param>
    public SysLogLoginController(ISysLogLoginService sysLogLoginService)
    {
        _sysLogLoginService = sysLogLoginService;
    }

    /// <summary>
    /// 批量删除系统登录日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [AppLog(Module = "系统登录日志", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteLogLogin(long[] logIds)
    {
        bool result = await _sysLogLoginService.DeleteLogLoginByIds(logIds);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 清空系统登录日志
    /// </summary>
    /// <returns></returns>
    [HttpDelete("Clean")]
    [AppLog(Module = "系统登录日志", BusinessType = BusinessTypeEnum.Clean)]
    public async Task<ApiResult> CleanLogLogin()
    {
        bool result = await _sysLogLoginService.CleanLogLogin();
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统登录日志(根据Id)
    /// </summary>
    /// <param name="logId"></param>
    /// <returns></returns>
    [HttpGet("Get/ById")]
    [AppLog(Module = "系统登录日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetLogLoginById(long logId)
    {
        SysLogLogin result = await _sysLogLoginService.GetLogLoginById(logId);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统登录日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    [HttpPost("GetList")]
    [AppLog(Module = "系统登录日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetLogLoginList([FromBody] SysLogLoginWDto whereDto)
    {
        List<SysLogLogin> result = await _sysLogLoginService.GetLogLoginList(whereDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统登录日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("GetPageList")]
    [AppLog(Module = "系统登录日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetLogLoginPageList([FromBody] PageWhereDto<SysLogLoginWDto> pageWhere)
    {
        PageDataDto<SysLogLogin> result = await _sysLogLoginService.GetLogLoginPageList(pageWhere);
        return ApiResult.Success(result);
    }
}