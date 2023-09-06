#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysEmailController
// Guid:60fad521-fe18-4a79-b7f6-af60b2fc44cf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/18 22:49:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Responses;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Emails;
using XiHan.Services.Syses.Emails.Dtos;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebHost.Controllers.Syses;

/// <summary>
/// 系统邮件管理
/// </summary>
[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysEmailController : BaseApiController
{
    private readonly ISysEmailService _sysEmailService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysEmailService"></param>
    public SysEmailController(ISysEmailService sysEmailService)
    {
        _sysEmailService = sysEmailService;
    }

    /// <summary>
    /// 新增系统邮件
    /// </summary>
    /// <param name="emailCDto"></param>
    /// <returns></returns>
    [HttpPost("Create")]
    [AppLog(Module = "系统邮件", BusinessType = BusinessTypeEnum.Create)]
    public async Task<ApiResult> CreateDictData([FromBody] SysEmailCDto emailCDto)
    {
        var result = await _sysEmailService.CreateSysEmail(emailCDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 删除系统邮件
    /// </summary>
    /// <param name="emailIds"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    [AppLog(Module = "系统邮件", BusinessType = BusinessTypeEnum.Delete)]
    public async Task<ApiResult> DeleteSysEmailByIds([FromBody] long[] emailIds)
    {
        var result = await _sysEmailService.DeleteSysEmailByIds(emailIds);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 修改系统邮件
    /// </summary>
    /// <param name="emailMDto"></param>
    /// <returns></returns>
    [HttpPut("Modify")]
    [AppLog(Module = "系统邮件", BusinessType = BusinessTypeEnum.Modify)]
    public async Task<ApiResult> ModifySysEmail([FromBody] SysEmailMDto emailMDto)
    {
        var result = await _sysEmailService.ModifySysEmail(emailMDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统邮件(根据Id)
    /// </summary>
    /// <param name="emailId"></param>
    /// <returns></returns>
    [HttpPost("Get/ById")]
    [AppLog(Module = "系统邮件", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSysEmailById([FromBody] long emailId)
    {
        var result = await _sysEmailService.GetSysEmailById(emailId);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统邮件列表
    /// </summary>
    /// <param name="emailWDto"></param>
    /// <returns></returns>
    [HttpPost("GetList")]
    [AppLog(Module = "系统邮件", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSysEmailList([FromBody] SysEmailWDto emailWDto)
    {
        var result = await _sysEmailService.GetSysEmailList(emailWDto);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 查询系统邮件列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    [HttpPost("GetPageList")]
    [AppLog(Module = "系统邮件", BusinessType = BusinessTypeEnum.Get)]
    public async Task<ApiResult> GetSysEmailPageList([FromBody] PageWhereDto<SysEmailWDto> pageWhere)
    {
        var result = await _sysEmailService.GetSysEmailPageList(pageWhere);
        return ApiResult.Success(result);
    }

    /// <summary>
    /// 导出系统邮件
    /// </summary>
    /// <returns></returns>
    [HttpGet("Export/Data")]
    [AppLog(Module = "系统邮件", BusinessType = BusinessTypeEnum.Export, IsSaveRequestData = false)]
    public async Task<ApiResult> ExportDict()
    {
        List<SysEmail> result = await _sysEmailService.GetListAsync();
        await ExportExcel("系统邮件", result, "SysEmail");
        return ApiResult.Success($"系统邮件导出成功！");
    }
}