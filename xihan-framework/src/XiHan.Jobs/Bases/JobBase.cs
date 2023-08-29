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
using XiHan.Services.Commons.Messages.EmailPush;
using XiHan.Services.Syses.Jobs;
using XiHan.Services.Syses.Logging;
using XiHan.Utils.Extensions;

namespace XiHan.Jobs.Bases;

/// <summary>
/// JobBase
/// </summary>
public class JobBase
{
    private readonly Stopwatch _stopwatch = new();
    private static readonly ILogger Logger = Log.ForContext<JobBase>();

    /// <summary>
    /// 执行指定任务
    /// </summary>
    /// <param name="context"></param>
    /// <param name="job"></param>
    /// <returns></returns>
    protected async Task ExecuteJob(IJobExecutionContext context, Func<Task> job)
    {
        // 记录Job日志
        SysLogJob sysLogJob = new();

        try
        {
            _stopwatch.Reset();
            await job();
            _stopwatch.Stop();

            sysLogJob.IsSuccess = true;
            sysLogJob.Message = "执行成功！";
            sysLogJob.Elapsed = _stopwatch.ElapsedMilliseconds;
        }
        catch (Exception ex)
        {
            _ = new JobExecutionException(ex)
            {
                // 立即重新执行任务
                RefireImmediately = true
            };

            sysLogJob.IsSuccess = false;
            sysLogJob.Message = "执行失败！";
            sysLogJob.Exception = ex.Message;
        }

        await RecordTasksLog(context, sysLogJob);
    }

    /// <summary>
    /// 记录任务日志
    /// </summary>
    /// <param name="context"></param>
    /// <param name="jobLog"></param>
    /// <returns></returns>
    private async Task RecordTasksLog(IJobExecutionContext context, SysLogJob jobLog)
    {
        try
        {
            ISysJobService sysJobService = App.GetRequiredService<ISysJobService>();
            ISysLogJobService sysLogJobService = App.GetRequiredService<ISysLogJobService>();
            IEmailPushService emailPushService = App.GetRequiredService<IEmailPushService>();

            // 获取任务详情
            IJobDetail jobDetail = context.JobDetail;
            jobLog.JobId = jobDetail.Key.Name.ParseToLong();
            jobLog.InvokeTarget = jobDetail.JobType.FullName;
            jobLog = await sysLogJobService.CreateLogJob(jobLog);
            string logInfo = $"执行任务【{jobDetail.Key.Name}|{jobLog.JobName}】，执行结果：{jobLog.Message}";

            // 若执行成功，则执行次数加一
            if (jobLog.IsSuccess)
            {
                _ = await sysJobService.UpdateAsync(job => new SysJob()
                {
                    RunTimes = job.RunTimes + 1,
                    LastRunTime = DateTime.Now
                }, f => f.BaseId == jobDetail.Key.Name.ParseToLong());
                Logger.Information(logInfo);
            }
            else
            {
                Logger.Error(logInfo);
                // 发送邮件
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, ex.Message);
            // 发送邮件
        }
    }
}