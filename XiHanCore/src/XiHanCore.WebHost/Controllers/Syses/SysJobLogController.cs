#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobLogController
// Guid:a6b72a71-814c-43ca-b83c-3313cf432b83
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-24 下午 04:45:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHanCore.Infrastructures.Apps.Logging;
using XiHanCore.Infrastructures.Responses;
using XiHanCore.Infrastructures.Responses.Pages;
using XiHanCore.Services.Syses.Jobs;
using XiHanCore.Services.Syses.Jobs.Dtos;
using XiHanCore.WebCore.Common.Swagger;

namespace XiHanCore.WebHost.Controllers.Syses;

/// <summary>
/// 系统任务日志管理
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="sysJobLogService"></param>
[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysJobLogController(ISysJobLogService sysJobLogService) : BaseApiController
{
    private readonly ISysJobLogService _sysJobLogService = sysJobLogService;

    /// <summary>
    /// 批量删除系统任务日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [AppLog(Module = "系统任务日志", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteJobLog(long[] ids)
    {
        var result = await _sysJobLogService.DeleteJobLogByIds(ids);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 清空系统任务日志
    /// </summary>
    [HttpDelete("Clean")]
    [AppLog(Module = "系统任务日志", BusinessType = BusinessTypeEnum.Clean)]
    public async Task<ApiResult> CleanJobLog()
    {
        var result = await _sysJobLogService.CleanJobLog();
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统任务日志(根据任务Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("GetByJobId")]
    [AppLog(Module = "系统任务日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetJobLogById(long id)
    {
        var result = await _sysJobLogService.GetJobLogByJobId(id);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统任务日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    [HttpPost("GetList")]
    [AppLog(Module = "系统任务日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetJobLogList([FromBody] SysJobLogWDto whereDto)
    {
        var result = await _sysJobLogService.GetJobLogList(whereDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统任务日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("GetPageList")]
    [AppLog(Module = "系统任务日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetJobLogPageList([FromBody] PageWhereDto<SysJobLogWDto> pageWhere)
    {
        var result = await _sysJobLogService.GetJobLogPageList(pageWhere);
        return ApiResult.Success(result);
    }
}