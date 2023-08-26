#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogJobController
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
using XiHan.Services.Syses.Logging;
using XiHan.Services.Syses.Logging.Dtos;
using XiHan.WebCore.Common.Swagger;
using XiHan.WebHost.Controllers.Bases;

namespace XiHan.WebHost.Controllers.ManageOrDisplay.Syses;

/// <summary>
/// 系统任务日志管理
/// </summary>
[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysLogJobController : BaseApiController
{
    private readonly ISysLogJobService _sysLogJobService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysLogJobService"></param>
    public SysLogJobController(ISysLogJobService sysLogJobService)
    {
        _sysLogJobService = sysLogJobService;
    }

    /// <summary>
    /// 批量删除系统任务日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [AppLog(Module = "系统任务日志", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteLogJob(long[] logIds)
    {
        var result = await _sysLogJobService.DeleteLogJobByIds(logIds);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 清空系统任务日志
    /// </summary>
    [HttpDelete("Clean")]
    [AppLog(Module = "系统任务日志", BusinessType = BusinessTypeEnum.Clean)]
    public async Task<ApiResult> CleanLogJob()
    {
        var result = await _sysLogJobService.CleanLogJob();
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统任务日志(根据任务Id)
    /// </summary>
    /// <param name="logId"></param>
    /// <returns></returns>
    [HttpGet("Get/ByJobId")]
    [AppLog(Module = "系统任务日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetLogJobById(long logId)
    {
        var result = await _sysLogJobService.GetLogJobByJobId(logId);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统任务日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    [HttpPost("GetList")]
    [AppLog(Module = "系统任务日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetLogJobList([FromBody] SysLogJobWDto whereDto)
    {
        var result = await _sysLogJobService.GetLogJobList(whereDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统任务日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("GetPageList")]
    [AppLog(Module = "系统任务日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetLogJobPageList([FromBody] PageWhereDto<SysLogJobWDto> pageWhere)
    {
        var result = await _sysLogJobService.GetLogJobPageList(pageWhere);
        return ApiResult.Success(result);
    }
}