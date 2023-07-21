#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:JobBase
// Guid:a910842d-d59b-44e0-90d5-057ad4fedf3d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/18 19:44:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Quartz;
using Serilog;
using System.Diagnostics;
using XiHan.Infrastructures.Apps;
using XiHan.Models.Syses;
using XiHan.Services.Syses.Tasks;
using XiHan.Utils.Extensions;

namespace XiHan.Tasks.Bases;

/// <summary>
/// JobBase
/// </summary>
public class JobBase
{
    private readonly Stopwatch _stopwatch = new();
    private static readonly ILogger _logger = Log.ForContext<JobBase>();

    /// <summary>
    /// 执行指定任务
    /// </summary>
    /// <param name="context"></param>
    /// <param name="job"></param>
    /// <returns></returns>
    public async Task ExecuteJob(IJobExecutionContext context, Func<Task> job)
    {
        // 记录Job日志
        var sysTasksLog = new SysTasksLog();

        try
        {
            _stopwatch.Reset();
            await job();
            _stopwatch.Stop();

            sysTasksLog.RunResult = true;
            sysTasksLog.TaskMessage = "执行成功！";
            sysTasksLog.Elapsed = _stopwatch.ElapsedMilliseconds;
        }
        catch (Exception ex)
        {
            _ = new JobExecutionException(ex)
            {
                // 立即重新执行任务
                RefireImmediately = true
            };

            sysTasksLog.RunResult = false;
            sysTasksLog.TaskMessage = "执行失败！";
            sysTasksLog.Exception = ex.Message;
        };

        await RecordTasksLog(context, sysTasksLog);
    }

    /// <summary>
    /// 记录任务日志
    /// </summary>
    /// <param name="context"></param>
    /// <param name="tasksLog"></param>
    /// <returns></returns>
    private async Task RecordTasksLog(IJobExecutionContext context, SysTasksLog tasksLog)
    {
        try
        {
            var _sysTasksService = App.GetRequiredService<ISysTasksService>();
            var _sysTasksLogService = App.GetRequiredService<ISysTasksLogService>();

            if (_sysTasksService != null && _sysTasksLogService != null)
            {
                // 获取任务详情
                IJobDetail jobDetail = context.JobDetail;
                tasksLog.TaskId = jobDetail.Key.Name.ParseToLong();
                tasksLog.InvokeTarget = jobDetail.JobType.FullName;
                tasksLog = await _sysTasksLogService.CreateTasksLog(tasksLog);
                var logInfo = $"执行任务【{jobDetail.Key.Name}|{tasksLog.TaskName}】，执行结果：{tasksLog.TaskMessage}";

                // 若执行成功，则执行次数加一
                if (tasksLog.RunResult)
                {
                    await _sysTasksService.UpdateAsync(j => new SysTasks()
                    {
                        RunTimes = j.RunTimes + 1,
                        LastRunTime = DateTime.Now
                    }, f => f.BaseId == jobDetail.Key.Name.ParseToLong());
                    _logger.Information(logInfo);
                }
                else
                {
                    _logger.Error(logInfo);
                }
            }
            else
            {
                throw new NotImplementedException($"服务【{nameof(ISysTasksService)}】或【{nameof(ISysTasksLogService)}】出错或未实现！");
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
        }
    }
}