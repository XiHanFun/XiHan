#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysEmailService
// Guid:c6b130b6-6e3a-4b38-bfba-f606efbc80d0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/9/3 0:40:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using SqlSugar;
using XiHanCore.Infrastructures.Apps.Services;
using XiHanCore.Infrastructures.Responses.Pages;
using XiHanCore.Models.Syses;
using XiHanCore.Services.Bases;
using XiHanCore.Services.Syses.Emails.Dtos;
using XiHanCore.Utils.Exceptions;
using XiHanCore.Utils.Extensions;

namespace XiHanCore.Services.Syses.Emails.Logic;

/// <summary>
/// 系统邮件服务
/// </summary>
[AppService(ServiceType = typeof(ISysEmailService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysEmailService : BaseService<SysEmail>, ISysEmailService
{
    /// <summary>
    /// 校验邮件项是否唯一
    /// </summary>
    /// <param name="sysEmail"></param>
    /// <returns></returns>
    private async Task<bool> CheckEmailUnique(SysEmail sysEmail)
    {
        var isUnique = await IsAnyAsync(f => f.Title == sysEmail.Title);
        return isUnique
            ? throw new CustomException($"邮件【{sysEmail.Title}】已存在!")
            : isUnique;
    }

    /// <summary>
    /// 新增系统邮件
    /// </summary>
    /// <param name="emailCDto"></param>
    /// <returns></returns>
    public async Task<long> CreateSysEmail(SysEmailCDto emailCDto)
    {
        var sysEmail = emailCDto.Adapt<SysEmail>();

        _ = await CheckEmailUnique(sysEmail);

        return await AddReturnIdAsync(sysEmail);
    }

    /// <summary>
    /// 批量删除系统邮件
    /// </summary>
    /// <param name="emailIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteSysEmailByIds(long[] emailIds)
    {
        List<SysEmail> sysEmailList = await QueryAsync(d => emailIds.Contains(d.BaseId));

        // 禁止删除正在使用的配置
        List<SysEmail> deleteList = sysEmailList.Where(c => !c.IsEnabled).ToList();
        return await RemoveAsync(deleteList);
    }

    /// <summary>
    /// 修改系统邮件
    /// </summary>
    /// <param name="emailMDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifySysEmail(SysEmailMDto emailMDto)
    {
        var sysEmail = emailMDto.Adapt<SysEmail>();
        _ = await CheckEmailUnique(sysEmail);
        return await UpdateAsync(sysEmail);
    }

    /// <summary>
    /// 查询系统邮件(根据Id)
    /// </summary>
    /// <param name="emailId"></param>
    /// <returns></returns>
    public async Task<SysEmail> GetSysEmailById(long emailId)
    {
        var sysEmail = await FindAsync(d => d.BaseId == emailId);
        return sysEmail;
    }

    /// <summary>
    /// 查询系统邮件列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysEmail>> GetSysEmailList(SysEmailWDto whereDto)
    {
        Expressionable<SysEmail> whereExpression = Expressionable.Create<SysEmail>();
        _ = whereExpression.AndIF(whereDto.Title.IsNotEmptyOrNull(), u => u.Title.Contains(whereDto.Title!));
        _ = whereExpression.AndIF(whereDto.IsEnabled != null, u => u.IsEnabled == whereDto.IsEnabled);

        return await QueryAsync(whereExpression.ToExpression(), o => o.Title, false);
    }

    /// <summary>
    /// 查询系统邮件列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysEmail>> GetSysEmailPageList(PageWhereDto<SysEmailWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        Expressionable<SysEmail> whereExpression = Expressionable.Create<SysEmail>();
        _ = whereExpression.AndIF(whereDto.Title.IsNotEmptyOrNull(), u => u.Title.Contains(whereDto.Title!));
        _ = whereExpression.AndIF(whereDto.IsEnabled != null, u => u.IsEnabled == whereDto.IsEnabled);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.Title, pageWhere.IsAsc);
    }
}