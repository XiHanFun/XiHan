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

using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Tasks.Logic;

/// <summary>
/// 系统任务日志服务
/// </summary>
[AppService(ServiceType = typeof(ISysTasksLogService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysTasksLogService : BaseService<SysTasksLog>, ISysTasksLogService
{
    private readonly ISysTasksService _sysTasksService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysTasksService"></param>
    public SysTasksLogService(ISysTasksService sysTasksService)
    {
        _sysTasksService = sysTasksService;
    }

    /// <summary>
    /// 新增任务日志
    /// </summary>
    /// <param name="jobId"></param>
    /// <param name="tasksLog"></param>
    /// <returns></returns>
    public async Task<SysTasksLog> CreateTasksLog(SysTasksLog tasksLog)
    {
        //获取任务信息
        var sysTasks = await _sysTasksService.GetSingleAsync(j => j.BaseId == tasksLog.JobId.ParseToLong());
        if (sysTasks != null)
        {
            tasksLog.JobName = sysTasks.JobName;
            tasksLog.JobGroup = sysTasks.JobGroup;
        }
        _ = await AddAsync(tasksLog);
        return tasksLog;
    }
}