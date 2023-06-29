#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysOperationLogService
// Guid:8bab505b-cf3a-4778-ae9c-df04b00f66a0
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-06-21 下午 05:43:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Repositories.Entities;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Operations.Logic;

/// <summary>
/// SysOperationLogService
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
    /// 查询系统操作日志集合
    /// </summary>
    /// <param name="sysOper">操作日志对象</param>
    /// <param name="pager"></param>
    /// <returns>操作日志集合</returns>
    public PagedInfo<SysOperLog> SelectOperLogList(SysOperLogDto sysOper, PagerInfo pager)
    {
        sysOper.BeginTime = DateTimeHelper.GetBeginTime(sysOper.BeginTime, -1);
        sysOper.EndTime = DateTimeHelper.GetBeginTime(sysOper.EndTime, 1);

        var exp = Expressionable.Create<SysOperLog>();
        exp.And(it => it.OperTime >= sysOper.BeginTime && it.OperTime <= sysOper.EndTime);
        exp.AndIF(sysOper.Title.IfNotEmpty(), it => it.Title.Contains(sysOper.Title));
        exp.AndIF(sysOper.OperName.IfNotEmpty(), it => it.OperName.Contains(sysOper.OperName));
        exp.AndIF(sysOper.BusinessType != -1, it => it.BusinessType == sysOper.BusinessType);
        exp.AndIF(sysOper.Status != -1, it => it.Status == sysOper.Status);
        exp.AndIF(sysOper.OperParam != null, it => it.OperParam.Contains(sysOper.OperParam));

        return GetPages(exp.ToExpression(), pager, x => x.OperId, OrderByType.Desc);
    }

    /// <summary>
    /// 查询操作日志详细
    /// </summary>
    /// <param name="operId">操作ID</param>
    /// <returns>操作日志对象</returns>
    public SysOperLog SelectOperLogById(long operId)
    {
        return GetById(operId);
    }
}