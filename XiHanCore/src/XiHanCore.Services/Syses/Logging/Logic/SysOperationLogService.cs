#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysOperationLogService
// Guid:8bab505b-cf3a-4778-ae9c-df04b00f66a0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-21 下午 05:43:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Infrastructures.Apps.Services;
using XiHanCore.Infrastructures.Responses.Pages;
using XiHanCore.Models.Syses;
using XiHanCore.Services.Bases;
using XiHanCore.Services.Syses.Logging.Dtos;
using XiHanCore.Utils.Extensions;

namespace XiHanCore.Services.Syses.Logging.Logic;

/// <summary>
/// 系统操作日志服务
/// </summary>
[AppService(ServiceType = typeof(ISysOperationLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysOperationLogService : BaseService<SysOperationLog>, ISysOperationLogService
{
    /// <summary>
    /// 新增系统操作日志
    /// </summary>
    /// <param name="logOperation"></param>
    /// <returns></returns>
    public async Task CreateOperationLog(SysOperationLog logOperation)
    {
        _ = await AddAsync(logOperation);
    }

    /// <summary>
    /// 批量删除系统操作日志
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteOperationLogByIds(long[] ids)
    {
        return await RemoveAsync(ids);
    }

    /// <summary>
    /// 清空系统操作日志
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CleanOperationLog()
    {
        return await CleanAsync();
    }

    /// <summary>
    /// 查询系统操作日志(根据Id)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<SysOperationLog> GetOperationLogById(long id)
    {
        return await FindAsync(id);
    }

    /// <summary>
    /// 查询系统操作日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysOperationLog>> GetOperationLogList(SysOperationLogWDto whereDto)
    {
        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysOperationLog> whereExpression = Expressionable.Create<SysOperationLog>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        _ = whereExpression.AndIF(whereDto.Module.IsNotEmptyOrNull(), l => l.Module == whereDto.Module);
        _ = whereExpression.AndIF(whereDto.BusinessType.IsNotEmptyOrNull(),
            l => l.BusinessType == whereDto.BusinessType);
        _ = whereExpression.AndIF(whereDto.RequestMethod.IsNotEmptyOrNull(),
            l => l.RequestMethod!.Equals(whereDto.RequestMethod, StringComparison.OrdinalIgnoreCase));
        _ = whereExpression.AndIF(whereDto.Status != null, l => l.Status == whereDto.Status);

        return await QueryAsync(whereExpression.ToExpression(), o => o.CreatedTime, false);
    }

    /// <summary>
    /// 查询系统操作日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysOperationLog>> GetOperationLogPageList(PageWhereDto<SysOperationLogWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        Expressionable<SysOperationLog> whereExpression = Expressionable.Create<SysOperationLog>();
        _ = whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        _ = whereExpression.AndIF(whereDto.Module.IsNotEmptyOrNull(), l => l.Module == whereDto.Module);
        _ = whereExpression.AndIF(whereDto.BusinessType.IsNotEmptyOrNull(),
            l => l.BusinessType == whereDto.BusinessType);
        _ = whereExpression.AndIF(whereDto.RequestMethod.IsNotEmptyOrNull(),
            l => l.RequestMethod!.Equals(whereDto.RequestMethod, StringComparison.OrdinalIgnoreCase));
        _ = whereExpression.AndIF(whereDto.Status != null, l => l.Status == whereDto.Status);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime,
            pageWhere.IsAsc);
    }
}