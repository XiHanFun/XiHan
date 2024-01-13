#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogController
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
using XiHanCore.Services.Syses.Emails;
using XiHanCore.Services.Syses.Emails.Dtos;
using XiHanCore.Services.Syses.Logging;
using XiHanCore.Services.Syses.Logging.Dtos;
using XiHanCore.Services.Syses.Smses;
using XiHanCore.Services.Syses.Smses.Dtos;
using XiHanCore.WebCore.Common.Swagger;

namespace XiHanCore.WebHost.Controllers.Syses;

/// <summary>
/// 系统日志管理
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="sysVisitLogService"></param>
/// <param name="sysOperationLogService"></param>
/// <param name="sysExceptionLogService"></param>
/// <param name="sysLoginLogService"></param>
/// <param name="sysSmsLogService"></param>
/// <param name="sysEmailLogService"></param>
[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysLogController(
    ISysVisitLogService sysVisitLogService,
    ISysOperationLogService sysOperationLogService,
    ISysExceptionLogService sysExceptionLogService,
    ISysLoginLogService sysLoginLogService,
    ISysSmsLogService sysSmsLogService,
    ISysEmailLogService sysEmailLogService) : BaseApiController
{
    private readonly ISysVisitLogService _sysVisitLogService = sysVisitLogService;
    private readonly ISysOperationLogService _sysOperationLogService = sysOperationLogService;
    private readonly ISysExceptionLogService _sysExceptionLogService = sysExceptionLogService;
    private readonly ISysLoginLogService _sysLoginLogService = sysLoginLogService;
    private readonly ISysSmsLogService _sysSmsLogService = sysSmsLogService;
    private readonly ISysEmailLogService _sysEmailLogService = sysEmailLogService;

    #region 访问日志

    /// <summary>
    /// 批量删除系统访问日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpDelete("Visit/Delete")]
    [AppLog(Module = "系统访问日志", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteVisitLog(long[] ids)
    {
        var result = await _sysVisitLogService.DeleteVisitLogByIds(ids);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 清空系统访问日志
    /// </summary>
    [HttpDelete("Visit/Clean")]
    [AppLog(Module = "系统访问日志", BusinessType = BusinessTypeEnum.Clean)]
    public async Task<ApiResult> CleanVisitLog()
    {
        var result = await _sysVisitLogService.CleanVisitLog();
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统访问日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Visit/GetById")]
    [AppLog(Module = "系统访问日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetVisitLogById(long id)
    {
        var result = await _sysVisitLogService.GetVisitLogById(id);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统访问日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    [HttpPost("Visit/GetList")]
    [AppLog(Module = "系统访问日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetVisitLogList([FromBody] SysVisitLogWDto whereDto)
    {
        var result = await _sysVisitLogService.GetVisitLogList(whereDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统访问日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("Visit/GetPageList")]
    [AppLog(Module = "系统访问日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetVisitLogPageList([FromBody] PageWhereDto<SysVisitLogWDto> pageWhere)
    {
        var result = await _sysVisitLogService.GetVisitLogPageList(pageWhere);
        return ApiResult.Success(result);
    }

    #endregion

    #region 操作日志

    /// <summary>
    /// 批量删除系统操作日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpDelete("Operation/Delete")]
    [AppLog(Module = "系统操作日志", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteOperationLog(long[] ids)
    {
        var result = await _sysOperationLogService.DeleteOperationLogByIds(ids);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 清空系统操作日志
    /// </summary>
    /// <returns></returns>
    [HttpDelete("Operation/Clean")]
    [AppLog(Module = "系统操作日志", BusinessType = BusinessTypeEnum.Clean)]
    public async Task<ApiResult> CleanOperationLog()
    {
        var result = await _sysOperationLogService.CleanOperationLog();
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统操作日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Operation/GetById")]
    [AppLog(Module = "系统操作日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetOperationLogById(long id)
    {
        var result = await _sysOperationLogService.GetOperationLogById(id);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统操作日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    [HttpPost("Operation/GetList")]
    [AppLog(Module = "系统操作日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetOperationLogList([FromBody] SysOperationLogWDto whereDto)
    {
        var result = await _sysOperationLogService.GetOperationLogList(whereDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统操作日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("Operation/GetPageList")]
    [AppLog(Module = "系统操作日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetOperationLogPageList([FromBody] PageWhereDto<SysOperationLogWDto> pageWhere)
    {
        var result = await _sysOperationLogService.GetOperationLogPageList(pageWhere);
        return ApiResult.Success(result);
    }

    #endregion

    #region 异常日志

    /// <summary>
    /// 批量删除系统异常日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpDelete("Exception/Delete")]
    [AppLog(Module = "系统异常日志", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteExceptionLog(long[] ids)
    {
        var result = await _sysExceptionLogService.DeleteExceptionLogByIds(ids);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 清空系统异常日志
    /// </summary>
    [HttpDelete("Exception/Clean")]
    [AppLog(Module = "系统异常日志", BusinessType = BusinessTypeEnum.Clean)]
    public async Task<ApiResult> CleanExceptionLog()
    {
        var result = await _sysExceptionLogService.CleanExceptionLog();
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统异常日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Exception/GetById")]
    [AppLog(Module = "系统异常日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetExceptionLogById(long id)
    {
        var result = await _sysExceptionLogService.GetExceptionLogById(id);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统异常日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    [HttpPost("Exception/GetList")]
    [AppLog(Module = "系统异常日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetExceptionLogList([FromBody] SysExceptionLogWDto whereDto)
    {
        var result = await _sysExceptionLogService.GetExceptionLogList(whereDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统异常日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("Exception/GetPageList")]
    [AppLog(Module = "系统异常日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetExceptionLogPageList([FromBody] PageWhereDto<SysExceptionLogWDto> pageWhere)
    {
        var result = await _sysExceptionLogService.GetExceptionLogPageList(pageWhere);
        return ApiResult.Success(result);
    }

    #endregion

    #region 登录日志

    /// <summary>
    /// 批量删除系统登录日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpDelete("Login/Delete")]
    [AppLog(Module = "系统登录日志", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteLoginLog(long[] ids)
    {
        var result = await _sysLoginLogService.DeleteLoginLogByIds(ids);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 清空系统登录日志
    /// </summary>
    /// <returns></returns>
    [HttpDelete("Login/Clean")]
    [AppLog(Module = "系统登录日志", BusinessType = BusinessTypeEnum.Clean)]
    public async Task<ApiResult> CleanLoginLog()
    {
        var result = await _sysLoginLogService.CleanLoginLog();
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统登录日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Login/GetById")]
    [AppLog(Module = "系统登录日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetLoginLogById(long id)
    {
        var result = await _sysLoginLogService.GetLoginLogById(id);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统登录日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    [HttpPost("Login/GetList")]
    [AppLog(Module = "系统登录日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetLoginLogList([FromBody] SysLoginLogWDto whereDto)
    {
        var result = await _sysLoginLogService.GetLoginLogList(whereDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统登录日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("Login/GetPageList")]
    [AppLog(Module = "系统登录日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetLoginLogPageList([FromBody] PageWhereDto<SysLoginLogWDto> pageWhere)
    {
        var result = await _sysLoginLogService.GetLoginLogPageList(pageWhere);
        return ApiResult.Success(result);
    }

    #endregion

    #region 短信日志

    /// <summary>
    /// 批量删除系统短信日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpDelete("Sms/Delete")]
    [AppLog(Module = "系统短信日志", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteSmsLog(long[] ids)
    {
        var result = await _sysSmsLogService.DeleteSmsLogByIds(ids);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 清空系统短信日志
    /// </summary>
    /// <returns></returns>
    [HttpDelete("Sms/Clean")]
    [AppLog(Module = "系统短信日志", BusinessType = BusinessTypeEnum.Clean)]
    public async Task<ApiResult> CleanSmsLog()
    {
        var result = await _sysSmsLogService.CleanSmsLog();
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统短信日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Sms/GetById")]
    [AppLog(Module = "系统短信日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSmsLogById(long id)
    {
        var result = await _sysSmsLogService.GetSmsLogById(id);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统短信日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    [HttpPost("Sms/GetList")]
    [AppLog(Module = "系统短信日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSmsLogList([FromBody] SysSmsLogWDto whereDto)
    {
        var result = await _sysSmsLogService.GetSmsLogList(whereDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统短信日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("Sms/GetPageList")]
    [AppLog(Module = "系统短信日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSmsLogPageList([FromBody] PageWhereDto<SysSmsLogWDto> pageWhere)
    {
        var result = await _sysSmsLogService.GetSmsLogPageList(pageWhere);
        return ApiResult.Success(result);
    }

    #endregion

    #region 邮件日志

    /// <summary>
    /// 批量删除系统邮件日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpDelete("Email/Delete")]
    [AppLog(Module = "系统邮件日志", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteEmailLog(long[] ids)
    {
        var result = await _sysEmailLogService.DeleteEmailLogByIds(ids);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 清空系统邮件日志
    /// </summary>
    /// <returns></returns>
    [HttpDelete("Email/Clean")]
    [AppLog(Module = "系统邮件日志", BusinessType = BusinessTypeEnum.Clean)]
    public async Task<ApiResult> CleanEmailLog()
    {
        var result = await _sysEmailLogService.CleanEmailLog();
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统邮件日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Email/GetById")]
    [AppLog(Module = "系统邮件日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetEmailLogById(long id)
    {
        var result = await _sysEmailLogService.GetEmailLogById(id);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统邮件日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    [HttpPost("Email/GetList")]
    [AppLog(Module = "系统邮件日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetEmailLogList([FromBody] SysEmailLogWDto whereDto)
    {
        var result = await _sysEmailLogService.GetEmailLogList(whereDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统邮件日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("Email/GetPageList")]
    [AppLog(Module = "系统邮件日志", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetEmailLogPageList([FromBody] PageWhereDto<SysEmailLogWDto> pageWhere)
    {
        var result = await _sysEmailLogService.GetEmailLogPageList(pageWhere);
        return ApiResult.Success(result);
    }

    #endregion
}