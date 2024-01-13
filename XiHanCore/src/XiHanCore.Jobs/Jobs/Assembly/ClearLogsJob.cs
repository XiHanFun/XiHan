#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:CleanLogsJob
// Guid:524456f9-1fd0-4c0f-aa7f-bc0f5889bcb5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 2:04:18
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Quartz;
using XiHanCore.Infrastructures.Apps.Services;
using XiHanCore.Jobs.Bases;
using XiHanCore.Services.Syses.Jobs;
using XiHanCore.Services.Syses.Logging;

namespace XiHanCore.Jobs.Jobs.Assembly;

/// <summary>
/// 清理日志任务
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="sysOperationLogService"></param>
/// <param name="sysJobLogService"></param>
[AppService(ServiceType = typeof(CleanLogsJob), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class CleanLogsJob(ISysOperationLogService sysOperationLogService, ISysJobLogService sysJobLogService) : JobBase, IJob
{
    private readonly ISysOperationLogService _sysOperationLogService = sysOperationLogService;
    private readonly ISysJobLogService _sysJobLogService = sysJobLogService;

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        await ExecuteJob(context, CleanLogs);
    }

    /// <summary>
    /// 清理日志
    /// </summary>
    /// <returns></returns>
    [Job(JobGroup = "系统", JobName = "清理日志", Description = "清理日志两个月前的操作日志和任务日志", IsEnable = true)]
    private async Task CleanLogs()
    {
        var twoMonthsAgo = DateTime.Now.AddMonths(-2);

        _ = await _sysOperationLogService.DeleteAsync(log => log.CreatedTime < twoMonthsAgo);
        _ = await _sysJobLogService.DeleteAsync(log => log.CreatedTime < twoMonthsAgo);
    }
}