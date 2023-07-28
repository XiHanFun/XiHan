#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ClearLogsJob
// Guid:524456f9-1fd0-4c0f-aa7f-bc0f5889bcb5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 2:04:18
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Quartz;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Services.Syses.Operations;
using XiHan.Jobs.Bases;
using XiHan.Services.Syses.Jobs;

namespace XiHan.Jobs.Jobs.Assembly;

/// <summary>
/// 清理日志任务
/// </summary>
[AppService(ServiceType = typeof(ClearLogsJob), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class ClearLogsJob : JobBase, IJob
{
    private readonly ISysOperationLogService _sysOperationLogService;
    private readonly ISysJobsLogService _sysJobsLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysOperationLogService"></param>
    /// <param name="sysJobsLogService"></param>
    public ClearLogsJob(ISysOperationLogService sysOperationLogService, ISysJobsLogService sysJobsLogService)
    {
        _sysOperationLogService = sysOperationLogService;
        _sysJobsLogService = sysJobsLogService;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        await base.ExecuteJob(context, async () => await ClearLogs());
    }

    /// <summary>
    /// 清理日志
    /// </summary>
    /// <returns></returns>
    [Job(JobGroup = "系统", JobName = "清理日志", Description = "清理日志两个月前的操作日志和任务日志", IsEnable = true)]
    public async Task ClearLogs()
    {
        var twoMonthsAgo = DateTime.Now.AddMonths(-2);

        await _sysOperationLogService.DeleteAsync(log => log.CreatedTime < twoMonthsAgo);
        await _sysJobsLogService.DeleteAsync(log => log.CreatedTime < twoMonthsAgo);
    }
}