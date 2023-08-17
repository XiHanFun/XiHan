#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:TaskSchedulerServer
// Guid:0415d360-cb23-4b9a-8f51-3636c2af2b72
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-11 下午 03:05:28
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;
using Quartz.Spi;
using SqlSugar;
using System.Collections.Specialized;
using System.Reflection;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Models.Syses.Enums;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;

namespace XiHan.Jobs.Bases.Servers;

/// <summary>
/// 任务调度管理中心
/// </summary>
public class TaskSchedulerServer : ITaskSchedulerServer
{
    private readonly IScheduler _scheduler;
    private readonly IJobFactory _jobFactory;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobFactory"></param>
    public TaskSchedulerServer(IJobFactory jobFactory)
    {
        _scheduler = GetTaskSchedulerAsync();
        _jobFactory = jobFactory;
    }

    /// <summary>
    /// 获取计划任务
    /// </summary>
    /// <returns></returns>
    private static IScheduler GetTaskSchedulerAsync()
    {
        // 从Factory中获取_scheduler实例
        var collection = new NameValueCollection()
        {
            { "quartz.serializer.type", "binary" }
        };
        var factory = new StdSchedulerFactory(collection);
        return factory.GetScheduler().Result;
    }

    #region 公共方法

    /// <summary>
    /// 添加一个计划任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<ApiResult> CreateTaskScheduleAsync(SysJob sysJob)
    {
        try
        {
            var jobKey = new JobKey(sysJob.BaseId.ToString(), sysJob.Group);
            if (await _scheduler.CheckExists(jobKey)) throw new Exception($"该计划任务已经在执行【{sysJob.Name}】,请勿重复添加！");

            // 判断任务类型，并创建一个任务，用于描述这个后台任务的详细信息
            var job = sysJob.JobType switch
            {
                // 程序集
                (int)JobTypeEnum.Assembly => CreateAssemblyJobDetail(sysJob),
                // 网络请求
                (int)JobTypeEnum.NetworkRequest => CreateNetworkRequestJobDetail(sysJob),
                // SQL语句类型
                (int)JobTypeEnum.SqlStatement => CreateSqlStatementJobDetail(sysJob),
                _ => throw new Exception("尝试创建系统未指定的任务类型！")
            };

            // 判断触发器类型，并创建一个触发器，指定任务的调度规则
            var trigger = sysJob.TriggerType switch
            {
                // 定时任务
                (int)TriggerTypeEnum.Interval => CreateIntervalTrigger(sysJob),
                // 时间点或者周期性任务
                (int)TriggerTypeEnum.Cron => CreateCronTrigger(sysJob),
                _ => throw new Exception("尝试创建系统未指定的触发器类型！")
            };

            // 判断任务调度是否开启，开启调度器
            if (!_scheduler.IsStarted) await StartTaskScheduleAsync();

            // 将触发器和任务器绑定到调度器中
            await _scheduler.ScheduleJob(job, trigger);
            // 按新的触发器重新设置任务执行
            await _scheduler.ResumeTrigger(trigger.Key);

            return ApiResult.Success($"添加计划【{sysJob.Name}】成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"添加计划任务【{sysJob.Name}】失败！", ex);
        }
    }

    /// <summary>
    /// 删除指定计划任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<ApiResult> DeleteTaskScheduleAsync(SysJob sysJob)
    {
        try
        {
            var jobKey = new JobKey(sysJob.BaseId.ToString(), sysJob.Group);
            await _scheduler.DeleteJob(jobKey);
            return ApiResult.Success($"删除计划任务【{sysJob.Name}】成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"删除计划任务【{sysJob.Name}】失败！", ex);
        }
    }

    /// <summary>
    /// 更新计划任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<ApiResult> ModifyTaskScheduleAsync(SysJob sysJob)
    {
        try
        {
            var jobKey = new JobKey(sysJob.BaseId.ToString(), sysJob.Group);
            if (await _scheduler.CheckExists(jobKey))
                // 防止创建时存在数据问题 先移除，然后在执行创建操作
                await _scheduler.DeleteJob(jobKey);
            return ApiResult.Success($"修改计划【{sysJob.Name}】成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"修改计划任务【{sysJob.Name}】失败！", ex);
        }
    }

    /// <summary>
    /// 开启计划任务
    /// </summary>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<ApiResult> StartTaskScheduleAsync()
    {
        try
        {
            _scheduler.JobFactory = _jobFactory;
            // 计划任务已经开启
            if (_scheduler.IsStarted) return ApiResult.Continue();

            // 等待任务运行完成
            await _scheduler.Start();
            return ApiResult.Success("开启计划任务成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"开启计划任务失败！", ex);
        }
    }

    /// <summary>
    /// 停止计划任务
    /// </summary>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<ApiResult> StopTaskScheduleAsync()
    {
        try
        {
            _scheduler.JobFactory = _jobFactory;
            // 计划任务已经停止
            if (_scheduler.IsShutdown) return ApiResult.Continue();
            // 等待任务运行停止
            await _scheduler.Shutdown();
            return ApiResult.Success($"停止计划任务成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException("停止计划任务失败！", ex);
        }
    }

    /// <summary>
    /// 立即运行指定计划任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<ApiResult> RunTaskScheduleAsync(SysJob sysJob)
    {
        try
        {
            var jobKey = new JobKey(sysJob.BaseId.ToString(), sysJob.Group);
            var jobs = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(sysJob.Group));
            List<JobKey> jobKeys = jobs.ToList();
            if (jobKeys.Any()) await CreateTaskScheduleAsync(sysJob);

            var triggers = await _scheduler.GetTriggersOfJob(jobKey);
            if (triggers.Count <= 0) return ApiResult.BadRequest($"未找到任务[{jobKey.Name}]的触发器！");

            await _scheduler.TriggerJob(jobKey);
            return ApiResult.Success($"计划任务[{jobKey.Name}]运行成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"执行计划任务【{sysJob.Name}】失败", ex);
        }
    }

    /// <summary>
    /// 暂停指定的计划任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<ApiResult> PauseTaskScheduleAsync(SysJob sysJob)
    {
        try
        {
            var jobKey = new JobKey(sysJob.BaseId.ToString(), sysJob.Group);
            if (await _scheduler.CheckExists(jobKey))
                // 防止创建时存在数据问题 先移除，然后在执行创建操作
                await _scheduler.PauseJob(jobKey);
            return ApiResult.Success($"暂停计划任务:【{sysJob.Name}】成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"暂停计划任务【{sysJob.Name}】失败！", ex);
        }
    }

    /// <summary>
    /// 恢复指定计划任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<ApiResult> ResumeTaskScheduleAsync(SysJob sysJob)
    {
        try
        {
            var jobKey = new JobKey(sysJob.BaseId.ToString(), sysJob.Group);
            if (!await _scheduler.CheckExists(jobKey))
                return ApiResult.BadRequest($"未找到计划任务【{sysJob.Name}】！");
            await _scheduler.ResumeJob(jobKey);
            return ApiResult.Success($"恢复计划任务【{sysJob.Name}】成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"恢复计划任务【{sysJob.Name}】失败！", ex);
        }
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 创建任务
    /// 程序集类型
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    private static IJobDetail CreateAssemblyJobDetail(SysJob sysJob)
    {
        if (sysJob.JobType != JobTypeEnum.Assembly.GetEnumValueByKey() || sysJob.AssemblyName == null || sysJob.ClassName == null)
            throw new AggregateException($"任务类型错误或缺少参数！");
        var assembly = Assembly.Load(new AssemblyName(sysJob.AssemblyName));
        var jobType = assembly.GetType(sysJob.AssemblyName + "." + sysJob.ClassName) ?? throw new AggregateException($"未找到该类型的任务计划！");
        // 传入执行程序集
        IJobDetail job = new JobDetailImpl(sysJob.Name, sysJob.Group, jobType);
        if (sysJob.Params != null) job.JobDataMap.Add("Param", sysJob.Params);
        return job;
    }

    /// <summary>
    /// 创建任务
    /// 网络请求类型
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    private static IJobDetail CreateNetworkRequestJobDetail(SysJob sysJob)
    {
        if (sysJob.JobType != JobTypeEnum.NetworkRequest.GetEnumValueByKey() || sysJob is not { RequestMethod: not null, ApiUrl: not null })
            throw new AggregateException($"任务类型错误或缺少参数！");
        var jobType = typeof(HttpClient);
        // 传入执行程序集
        IJobDetail job = new JobDetailImpl(sysJob.BaseId.ToString(), sysJob.Group, jobType);
        return job;
    }

    /// <summary>
    /// 创建任务
    /// SQL 语句类型
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    private static IJobDetail CreateSqlStatementJobDetail(SysJob sysJob)
    {
        if (sysJob.JobType != JobTypeEnum.SqlStatement.GetEnumValueByKey() || sysJob.SqlText == null)
            throw new AggregateException($"任务类型错误或缺少参数！");
        var jobType = typeof(SqlSugarClient);
        // 传入执行程序集
        IJobDetail job = new JobDetailImpl(sysJob.BaseId.ToString(), sysJob.Group, jobType);
        return job;
    }

    /// <summary>
    /// 创建 Interval 类型的触发器
    /// 定时任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    private static ITrigger CreateIntervalTrigger(SysJob sysJob)
    {
        if (sysJob.TriggerType != TriggerTypeEnum.Interval.GetEnumValueByKey())
            throw new AggregateException($"触发器类型错误或触发条件未通过验证！");
        // 设置开始时间和结束时间
        sysJob.BeginTime ??= DateTime.Now;
        sysJob.EndTime ??= DateTime.MaxValue.AddDays(-1);
        var starRunTime = DateBuilder.NextGivenSecondDate(sysJob.BeginTime, 1);
        var endRunTime = DateBuilder.NextGivenSecondDate(sysJob.EndTime, 1);

        if (sysJob.EndTime <= DateTime.Now)
            throw new Exception($"结束时间小于当前时间计划将不会被执行！");
        if (sysJob.CycleRunTimes != 0 && sysJob.CycleHasRunTimes >= sysJob.CycleRunTimes)
            throw new Exception($"该任务计划已完成:【{sysJob.Name}】,无需重复启动,如需启动请修改已循环次数再提交");

        // 触发作业立即运行，然后每N秒重复一次，无限循环
        if (sysJob.RunTimes > 0)
        {
            var trigger = TriggerBuilder.Create()
                .WithIdentity(sysJob.BaseId.ToString(), sysJob.Group)
                .StartAt(starRunTime)
                .EndAt(endRunTime)
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(sysJob.IntervalSecond)
                    .WithRepeatCount(sysJob.RunTimes))
                .ForJob(sysJob.BaseId.ToString(), sysJob.Group)
                .Build();
            return trigger;
        }
        else
        {
            var trigger = TriggerBuilder.Create()
                .WithIdentity(sysJob.BaseId.ToString(), sysJob.Group)
                .StartAt(starRunTime)
                .EndAt(endRunTime)
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(sysJob.IntervalSecond)
                    .RepeatForever())
                .ForJob(sysJob.BaseId.ToString(), sysJob.Group)
                .Build();
            return trigger;
        }
    }

    /// <summary>
    /// 创建 Cron 类型的触发器
    /// 时间点或者周期性任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    private static ITrigger CreateCronTrigger(SysJob sysJob)
    {
        if (sysJob.TriggerType != TriggerTypeEnum.Cron.GetEnumValueByKey())
            throw new AggregateException($"触发器类型错误或触发条件未通过验证！");
        // 设置开始时间和结束时间
        sysJob.BeginTime ??= DateTime.Now;
        sysJob.EndTime ??= DateTime.MaxValue.AddDays(-1);
        var starRunTime = DateBuilder.NextGivenSecondDate(sysJob.BeginTime, 1);
        var endRunTime = DateBuilder.NextGivenSecondDate(sysJob.EndTime, 1);

        if (sysJob.Cron == null || !CronExpression.IsValidExpression(sysJob.Cron))
            throw new AggregateException($"触发器类型错误或触发条件未通过验证！");
        // 作业触发器
        var trigger = TriggerBuilder.Create()
            .WithIdentity(sysJob.BaseId.ToString(), sysJob.Group)
            .StartAt(starRunTime)
            .EndAt(endRunTime)
            .WithCronSchedule(sysJob.Cron)
            .ForJob(sysJob.BaseId.ToString(), sysJob.Group)
            .Build();
        // 解决 Quartz 启动后第一次会立即执行的办法
        ((CronTriggerImpl)trigger).MisfireInstruction = MisfireInstruction.CronTrigger.DoNothing;
        return trigger;
    }

    #endregion
}