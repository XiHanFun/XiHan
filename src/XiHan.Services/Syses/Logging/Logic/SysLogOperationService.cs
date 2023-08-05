#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogOperationService
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
using XiHan.Services.Syses.Logging.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Logging.Logic;

/// <summary>
/// 系统操作日志服务
/// </summary>
[AppService(ServiceType = typeof(ISysLogOperationService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysLogOperationService : BaseService<SysLogOperation>, ISysLogOperationService
{
    /// <summary>
    /// 新增系统操作日志
    /// </summary>
    /// <param name="logOperation"></param>
    /// <returns></returns>
    public async Task CreateLogOperation(SysLogOperation logOperation)
    {
        await AddAsync(logOperation);
    }

    /// <summary>
    /// 批量删除系统操作日志
    /// </summary>
    /// <param name="logIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteLogOperationByIds(long[] logIds)
    {
        return await RemoveAsync(logIds);
    }

    /// <summary>
    /// 清空系统操作日志
    /// </summary>
    /// <returns></returns>
    public async Task<bool> ClearLogOperation()
    {
        return await ClearAsync();
    }

    /// <summary>
    /// 查询系统操作日志(根据Id)
    /// </summary>
    /// <param name="logId"></param>
    /// <returns></returns>
    public async Task<SysLogOperation> GetLogOperationById(long logId)
    {
        return await FindAsync(logId);
    }

    /// <summary>
    /// 查询系统操作日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysLogOperation>> GetLogOperationList(SysLogOperationWDto whereDto)
    {
        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime().GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        var whereExpression = Expressionable.Create<SysLogOperation>();
        whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        whereExpression.AndIF(whereDto.Module.IsNotEmptyOrNull(), l => l.Module == whereDto.Module);
        whereExpression.AndIF(whereDto.BusinessType.IsNotEmptyOrNull(), l => l.BusinessType == whereDto.BusinessType);
        whereExpression.AndIF(whereDto.RequestMethod.IsNotEmptyOrNull(), l => l.RequestMethod!.ToUpperInvariant() == whereDto.RequestMethod!.ToUpperInvariant());
        whereExpression.AndIF(whereDto.Status != null, l => l.Status == whereDto.Status);

        return await QueryAsync(whereExpression.ToExpression(), o => o.CreatedTime, false);
    }

    /// <summary>
    /// 查询系统操作日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysLogOperation>> GetLogOperationPageList(PageWhereDto<SysLogOperationWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        // 时间为空，默认查询当天
        whereDto.BeginTime ??= whereDto.BeginTime.GetBeginTime(0).GetDayMinDate();
        whereDto.EndTime ??= whereDto.BeginTime.Value.AddDays(1);

        var whereExpression = Expressionable.Create<SysLogOperation>();
        whereExpression.And(l => l.CreatedTime >= whereDto.BeginTime && l.CreatedTime < whereDto.EndTime);
        whereExpression.AndIF(whereDto.Module.IsNotEmptyOrNull(), l => l.Module == whereDto.Module);
        whereExpression.AndIF(whereDto.BusinessType.IsNotEmptyOrNull(), l => l.BusinessType == whereDto.BusinessType);
        whereExpression.AndIF(whereDto.RequestMethod.IsNotEmptyOrNull(), l => l.RequestMethod!.ToUpperInvariant() == whereDto.RequestMethod!.ToUpperInvariant());
        whereExpression.AndIF(whereDto.Status != null, l => l.Status == whereDto.Status);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime, false);
    }
}