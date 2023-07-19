#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLoginLogService
// Guid:20f98d51-1827-4403-9021-b4fe7953a683
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-19 下午 02:55:04
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Logins.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Logins.Logic;

/// <summary>
/// 系统登录日志服务
/// </summary>
[AppService(ServiceType = typeof(ISysLoginLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysLoginLogService : BaseService<SysLoginLog>, ISysLoginLogService
{
    /// <summary>
    /// 新增登录日志
    /// </summary>
    /// <param name="loginLog"></param>
    /// <returns></returns>
    public async Task CreateLoginLog(SysLoginLog loginLog)
    {
        await AddAsync(loginLog);
    }

    /// <summary>
    /// 批量删除登录日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteLoginLogByIds(long[] logIds)
    {
        return await RemoveAsync(logIds);
    }

    /// <summary>
    /// 清空登录日志
    /// </summary>
    public async Task<bool> CleanLoginLog()
    {
        return await ClearAsync();
    }

    /// <summary>
    /// 查询登录日志(根据Id)
    /// </summary>
    /// <param name="logId"></param>
    /// <returns></returns>
    public async Task<SysLoginLog> GetLoginLogById(long logId)
    {
        return await FindAsync(logId);
    }

    /// <summary>
    /// 查询登录日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysLoginLog>> GetLoginLogBList(PageWhereDto<SysLoginLogWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime(-1);
        whereDto.EndTime ??= whereDto.EndTime.GetBeginTime(1);

        var whereExpression = Expressionable.Create<SysLoginLog>();
        whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        whereExpression.AndIF(whereDto.LoginIp.IsNotEmptyOrNull(), l => l.LoginIp!.Contains(whereDto.LoginIp!));
        whereExpression.AndIF(whereDto.Account.IsNotEmptyOrNull(), l => l.Account!.Contains(whereDto.Account!));
        whereExpression.AndIF(whereDto.RealName.IsNotEmptyOrNull(), l => l.RealName!.Contains(whereDto.RealName!));
        whereExpression.AndIF(whereDto.Status != null, l => l.Status == whereDto.Status);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime, false);
    }
}