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
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Operations.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Operations.Logic;

/// <summary>
/// 系统操作日志服务
/// </summary>
[AppService(ServiceType = typeof(ISysOperationLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysOperationLogService : BaseService<SysOperationLog>, ISysOperationLogService
{
    /// <summary>
    /// 新增操作日志
    /// </summary>
    /// <param name="operationLog"></param>
    /// <returns></returns>
    public async Task CreateOperationLog(SysOperationLog operationLog)
    {
        await AddAsync(operationLog);
    }

    /// <summary>
    /// 批量删除操作日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteOperationLogByIds(long[] logIds)
    {
        return await RemoveAsync(logIds);
    }

    /// <summary>
    /// 清空操作日志
    /// </summary>
    public async Task<bool> ClearOperationLog()
    {
        return await ClearAsync();
    }

    /// <summary>
    /// 查询操作日志(根据Id)
    /// </summary>
    /// <param name="logId"></param>
    /// <returns></returns>
    public async Task<SysOperationLog> GetOperationLogById(long logId)
    {
        return await FindAsync(logId);
    }

    /// <summary>
    /// 查询操作日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysOperationLog>> GetOperationLogByList(PageWhereDto<SysOperationLogWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime(0).GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        var whereExpression = Expressionable.Create<SysOperationLog>();
        whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        whereExpression.AndIF(whereDto.Module.IsNotEmptyOrNull(), l => l.Module == whereDto.Module);
        whereExpression.AndIF(whereDto.BusinessType.IsNotEmptyOrNull(), l => l.BusinessType == whereDto.BusinessType);
        whereExpression.AndIF(whereDto.RequestMethod.IsNotEmptyOrNull(), l => l.RequestMethod!.ToUpperInvariant() == whereDto.RequestMethod!.ToUpperInvariant());
        whereExpression.AndIF(whereDto.Status != null, l => l.Status == whereDto.Status);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime);
    }
}