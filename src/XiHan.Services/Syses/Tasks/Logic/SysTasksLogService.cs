#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysTasksLogService
// Guid:65179156-6084-40b4-ace3-0cda5ee49eeb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 1:16:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructures.Apps.Caches;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Tasks.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Tasks.Logic;

/// <summary>
/// 系统任务日志服务
/// </summary>
[AppService(ServiceType = typeof(ISysTasksLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysTasksLogService : BaseService<SysTasksLog>, ISysTasksLogService
{
    private readonly IAppCacheService _appCacheService;
    private readonly ISysTasksService _sysTasksService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="appCacheService"></param>
    /// <param name="sysTasksService"></param>
    public SysTasksLogService(IAppCacheService appCacheService, ISysTasksService sysTasksService)
    {
        _appCacheService = appCacheService;
        _sysTasksService = sysTasksService;
    }

    /// <summary>
    /// 新增任务日志
    /// </summary>
    /// <param name="tasksLog"></param>
    /// <returns></returns>
    public async Task<SysTasksLog> CreateTasksLog(SysTasksLog tasksLog)
    {
        //获取任务信息
        var sysTasks = await _sysTasksService.GetSingleAsync(j => j.BaseId == tasksLog.TaskId);
        if (sysTasks != null)
        {
            tasksLog.TaskName = sysTasks.TaskName;
            tasksLog.TaskGroup = sysTasks.TaskGroup;
        }
        _ = await AddAsync(tasksLog);
        return tasksLog;
    }

    /// <summary>
    /// 查询系统任务日志(根据任务Id)
    /// </summary>
    /// <param name="tasksId"></param>
    /// <returns></returns>
    public async Task<SysTasksLog> GetTasksLogByTaskId(long tasksId)
    {
        var key = $"GetTasksLogByTaskId_{tasksId}";
        if (_appCacheService.Get(key) is SysTasksLog sysTasksLog) return sysTasksLog;
        sysTasksLog = await FindAsync(d => d.TaskId == tasksId);
        _appCacheService.SetWithMinutes(key, sysTasksLog, 30);

        return sysTasksLog;
    }

    /// <summary>
    /// 查询系统任务日志列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    public async Task<List<SysTasksLog>> GetTasksLogList(SysTasksLogWDto whereDto)
    {
        var whereExpression = Expressionable.Create<SysTasksLog>();
        whereExpression.AndIF(whereDto.TaskName.IsNotEmptyOrNull(), u => u.TaskName.Contains(whereDto.TaskName!));
        whereExpression.AndIF(whereDto.TaskGroup.IsNotEmptyOrNull(), u => u.TaskGroup.Contains(whereDto.TaskGroup!));
        whereExpression.AndIF(whereDto.RunResult != null, u => u.RunResult == whereDto.RunResult);

        return await QueryAsync(whereExpression.ToExpression(), o => o.CreatedTime, false);
    }

    /// <summary>
    /// 查询系统任务日志列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysTasksLog>> GetTasksLogPageList(PageWhereDto<SysTasksLogWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        var whereExpression = Expressionable.Create<SysTasksLog>();
        whereExpression.AndIF(whereDto.TaskName.IsNotEmptyOrNull(), u => u.TaskName.Contains(whereDto.TaskName!));
        whereExpression.AndIF(whereDto.TaskGroup.IsNotEmptyOrNull(), u => u.TaskGroup.Contains(whereDto.TaskGroup!));
        whereExpression.AndIF(whereDto.RunResult != null, u => u.RunResult == whereDto.RunResult);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.CreatedTime, false);
    }
}