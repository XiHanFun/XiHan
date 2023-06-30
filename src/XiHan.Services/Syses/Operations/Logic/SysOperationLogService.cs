#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysOperationLogService
// Guid:8bab505b-cf3a-4778-ae9c-df04b00f66a0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-21 下午 05:43:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Repositories.Entities;
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
    /// <param name="sysOperationLog"></param>
    /// <returns></returns>
    public async Task CreateOperationLog(SysOperationLog sysOperationLog)
    {
        sysOperationLog = sysOperationLog.ToCreated();
        await AddAsync(sysOperationLog);
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
    public async Task<bool> CleanOperationLog()
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
    public async Task<PageDataDto<SysOperationLog>> GetOperationLogBList(PageWhereDto<SysOperationLogWhereDto> pageWhere)
    {
        var whereDto = pageWhere.Where;
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime(-1);
        whereDto.EndTime ??= whereDto.EndTime.GetBeginTime(1);

        var whereExpression = Expressionable.Create<SysOperationLog>();
        whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        whereExpression.AndIF(whereDto.Module.IsNotEmptyOrNull(), l => l.Module == whereDto.Module);
        whereExpression.AndIF(whereDto.BusinessType.IsNotEmptyOrNull(), l => l.BusinessType == whereDto.BusinessType);
        whereExpression.AndIF(whereDto.RequestMethod.IsNotEmptyOrNull(), l => l.RequestMethod == whereDto.RequestMethod);
        whereExpression.AndIF(whereDto.Status != null, l => l.Status == whereDto.Status);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime);
    }
}