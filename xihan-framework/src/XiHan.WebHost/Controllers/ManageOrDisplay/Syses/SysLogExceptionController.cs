#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogExceptionController
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
/// 系统异常日志管理
/// </summary>
[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysLogExceptionController : BaseApiController
{
    private readonly ISysLogExceptionService _sysLogExceptionService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysLogExceptionService"></param>
    public SysLogExceptionController(ISysLogExceptionService sysLogExceptionService)
    {
        _sysLogExceptionService = sysLogExceptionService;
    }

    /// <summary>
    /// 批量删除系统异常日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [AppLog(Module = "系统异常日志", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteLogException(long[] logIds)
    {
        bool result = await _sysLogExceptionService.DeleteLogExceptionByIds(logIds);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 清空系统异常日志
    /// </summary>
    [HttpDelete("Clean")]
    [AppLog(Module = "系统异常日志", BusinessType = BusinessTypeEnum.Clean)]
    public async Task<ApiResult> CleanLogException()
    {
        bool result = await _sysLogExceptionService.CleanLogException();
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统异常日志(根据Id)
    /// </summary>
    /// <param name="logId"></param>
    /// <returns></returns>
    [HttpGet("Get/ById")]
    [AppLog(Module = "系统异常日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetLogExceptionById(long logId)
    {
        Models.Syses.SysLogException result = await _sysLogExceptionService.GetLogExceptionById(logId);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统异常日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    [HttpPost("GetList")]
    [AppLog(Module = "系统异常日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetLogExceptionList([FromBody] SysLogExceptionWDto whereDto)
    {
        List<Models.Syses.SysLogException> result = await _sysLogExceptionService.GetLogExceptionList(whereDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统异常日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("GetPageList")]
    [AppLog(Module = "系统异常日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetLogExceptionPageList([FromBody] PageWhereDto<SysLogExceptionWDto> pageWhere)
    {
        PageDataDto<Models.Syses.SysLogException> result = await _sysLogExceptionService.GetLogExceptionPageList(pageWhere);
        return ApiResult.Success(result);
    }
}