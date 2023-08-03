#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysOperationLogController
// Guid:a6b72a71-814c-43ca-b83c-3313cf432b83
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-24 下午 04:45:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Operations;
using XiHan.Services.Syses.Operations.Dtos;
using XiHan.WebApi.Controllers.Bases;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebApi.Controllers.ManageOrDisplay.Syses;

/// <summary>
/// 系统操作日志管理
/// </summary>
[Authorize]
[ApiGroup(ApiGroupNames.Manage)]
public class SysOperationLogController : BaseApiController
{
    private readonly ISysOperationLogService _sysOperationLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysOperationLogService"></param>
    public SysOperationLogController(ISysOperationLogService sysOperationLogService)
    {
        _sysOperationLogService = sysOperationLogService;
    }

    /// <summary>
    /// 批量删除操作日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [AppLog(Module = "系统操作日志", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<bool> DeleteOperationLog(long[] logIds)
    {
        return await _sysOperationLogService.DeleteOperationLogByIds(logIds);
    }

    /// <summary>
    /// 清空操作日志
    /// </summary>
    [HttpDelete("Clear")]
    [AppLog(Module = "系统操作日志", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<bool> ClearOperationLog()
    {
        return await _sysOperationLogService.ClearOperationLog();
    }

    /// <summary>
    /// 查询操作日志(根据Id)
    /// </summary>
    /// <param name="logId"></param>
    /// <returns></returns>
    [HttpGet("Get/ById")]
    public async Task<SysOperationLog> GetOperationLogById(long logId)
    {
        return await _sysOperationLogService.GetOperationLogById(logId);
    }

    /// <summary>
    /// 查询操作日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpGet("GetPageList")]
    public async Task<PageDataDto<SysOperationLog>> GetOperationLogByList(PageWhereDto<SysOperationLogWDto> pageWhere)
    {
        return await _sysOperationLogService.GetOperationLogByList(pageWhere);
    }
}