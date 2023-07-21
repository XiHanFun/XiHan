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
using XiHan.Infrastructures.Exceptions;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Models.Syses.Enums;
using XiHan.Utils.Extensions;

namespace XiHan.Tasks.Bases.Servers;

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
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<CustomResult> CreateTaskScheduleAsync(SysTasks sysTasks)
    {
        try
        {
            var jobKey = new JobKey(sysTasks.BaseId.ToString(), sysTasks.TaskGroup);
            if (await _scheduler.CheckExists(jobKey)) throw new Exception($"该计划任务已经在执行【{sysTasks.TaskName}】,请勿重复添加！");

            // 判断任务类型，并创建一个任务，用于描述这个后台任务的详细信息
            var job = sysTasks.JobType switch
            {
                // 程序集
                (int)JobTypeEnum.Assembly => CreateAssemblyJobDetail(sysTasks),
                // 网络请求
                (int)JobTypeEnum.NetworkRequest => CreateNetworkRequestJobDetail(sysTasks),
                // SQL语句类型
                (int)JobTypeEnum.SqlStatement => CreateSqlStatementJobDetail(sysTasks),
                _ => throw new Exception("尝试创建系统未指定的任务类型！")
            };

            // 判断触发器类型，并创建一个触发器，指定任务的调度规则
            var trigger = sysTasks.TriggerType switch
            {
                // 定时任务
                (int)TriggerTypeEnum.Interval => CreateIntervalTrigger(sysTasks),
                // 时间点或者周期性任务
                (int)TriggerTypeEnum.Cron => CreateCronTrigger(sysTasks),
                _ => throw new Exception("尝试创建系统未指定的触发器类型！")
            };

            // 判断任务调度是否开启，开启调度器
            if (!_scheduler.IsStarted) await StartTaskScheduleAsync();

            // 将触发器和任务器绑定到调度器中
            await _scheduler.ScheduleJob(job, trigger);
            // 按新的触发器重新设置任务执行
            await _scheduler.ResumeTrigger(trigger.Key);

            return CustomResult.Success($"添加计划【{sysTasks.TaskName}】成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"添加计划任务【{sysTasks.TaskName}】失败！", ex);
        }
    }

    /// <summary>
    /// 删除指定计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<CustomResult> DeleteTaskScheduleAsync(SysTasks sysTasks)
    {
        try
        {
            var jobKey = new JobKey(sysTasks.BaseId.ToString(), sysTasks.TaskGroup);
            await _scheduler.DeleteJob(jobKey);
            return CustomResult.Success($"删除计划任务【{sysTasks.TaskName}】成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"删除计划任务【{sysTasks.TaskName}】失败！", ex);
        }
    }

    /// <summary>
    /// 更新计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<CustomResult> ModifyTaskScheduleAsync(SysTasks sysTasks)
    {
        try
        {
            var jobKey = new JobKey(sysTasks.BaseId.ToString(), sysTasks.TaskGroup);
            if (await _scheduler.CheckExists(jobKey))
                // 防止创建时存在数据问题 先移除，然后在执行创建操作
                await _scheduler.DeleteJob(jobKey);
            return CustomResult.Success($"修改计划【{sysTasks.TaskName}】成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"修改计划任务【{sysTasks.TaskName}】失败！", ex);
        }
    }

    /// <summary>
    /// 开启计划任务
    /// </summary>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<CustomResult> StartTaskScheduleAsync()
    {
        try
        {
            _scheduler.JobFactory = _jobFactory;
            // 计划任务已经开启
            if (_scheduler.IsStarted) return CustomResult.Continue();

            // 等待任务运行完成
            await _scheduler.Start();
            return CustomResult.Success("开启计划任务成功！");
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
    public async Task<CustomResult> StopTaskScheduleAsync()
    {
        try
        {
            _scheduler.JobFactory = _jobFactory;
            // 计划任务已经停止
            if (_scheduler.IsShutdown) return CustomResult.Continue();
            // 等待任务运行停止
            await _scheduler.Shutdown();
            return CustomResult.Success($"停止计划任务成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException("停止计划任务失败！", ex);
        }
    }

    /// <summary>
    /// 立即运行指定计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<CustomResult> RunTaskScheduleAsync(SysTasks sysTasks)
    {
        try
        {
            var jobKey = new JobKey(sysTasks.BaseId.ToString(), sysTasks.TaskGroup);
            var jobs = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(sysTasks.TaskGroup));
            List<JobKey> jobKeys = jobs.ToList();
            if (jobKeys.Any()) await CreateTaskScheduleAsync(sysTasks);

            var triggers = await _scheduler.GetTriggersOfJob(jobKey);
            if (triggers.Count <= 0) return CustomResult.BadRequest($"未找到任务[{jobKey.Name}]的触发器！");

            await _scheduler.TriggerJob(jobKey);
            return CustomResult.Success($"计划任务[{jobKey.Name}]运行成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"执行计划任务【{sysTasks.TaskName}】失败", ex);
        }
    }

    /// <summary>
    /// 暂停指定的计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<CustomResult> PauseTaskScheduleAsync(SysTasks sysTasks)
    {
        try
        {
            var jobKey = new JobKey(sysTasks.BaseId.ToString(), sysTasks.TaskGroup);
            if (await _scheduler.CheckExists(jobKey))
                // 防止创建时存在数据问题 先移除，然后在执行创建操作
                await _scheduler.PauseJob(jobKey);
            return CustomResult.Success($"暂停计划任务:【{sysTasks.TaskName}】成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"暂停计划任务【{sysTasks.TaskName}】失败！", ex);
        }
    }

    /// <summary>
    /// 恢复指定计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<CustomResult> ResumeTaskScheduleAsync(SysTasks sysTasks)
    {
        try
        {
            var jobKey = new JobKey(sysTasks.BaseId.ToString(), sysTasks.TaskGroup);
            if (!await _scheduler.CheckExists(jobKey))
                return CustomResult.BadRequest($"未找到计划任务【{sysTasks.TaskName}】！");
            await _scheduler.ResumeJob(jobKey);
            return CustomResult.Success($"恢复计划任务【{sysTasks.TaskName}】成功！");
        }
        catch (Exception ex)
        {
            throw new CustomException($"恢复计划任务【{sysTasks.TaskName}】失败！", ex);
        }
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 创建任务
    /// 程序集类型
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    private static IJobDetail CreateAssemblyJobDetail(SysTasks sysTasks)
    {
        if (sysTasks.JobType != JobTypeEnum.Assembly.GetEnumValueByKey() || sysTasks.AssemblyName == null || sysTasks.ClassName == null)
            throw new AggregateException($"任务类型错误或缺少参数！");
        var assembly = Assembly.Load(new AssemblyName(sysTasks.AssemblyName));
        var jobType = assembly.GetType(sysTasks.AssemblyName + "." + sysTasks.ClassName) ?? throw new AggregateException($"未找到该类型的任务计划！");
        // 传入执行程序集
        IJobDetail job = new JobDetailImpl(sysTasks.TaskName, sysTasks.TaskGroup, jobType);
        if (sysTasks.JobParams != null) job.JobDataMap.Add("JobParam", sysTasks.JobParams);
        return job;
    }

    /// <summary>
    /// 创建任务
    /// 网络请求类型
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    private static IJobDetail CreateNetworkRequestJobDetail(SysTasks sysTasks)
    {
        if (sysTasks.JobType != JobTypeEnum.NetworkRequest.GetEnumValueByKey() || sysTasks is not { RequestMethod: not null, ApiUrl: not null })
            throw new AggregateException($"任务类型错误或缺少参数！");
        var jobType = typeof(HttpClient);
        // 传入执行程序集
        IJobDetail job = new JobDetailImpl(sysTasks.BaseId.ToString(), sysTasks.TaskGroup, jobType);
        return job;
    }

    /// <summary>
    /// 创建任务
    /// SQL 语句类型
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    private static IJobDetail CreateSqlStatementJobDetail(SysTasks sysTasks)
    {
        if (sysTasks.JobType != JobTypeEnum.SqlStatement.GetEnumValueByKey() || sysTasks.SqlText == null)
            throw new AggregateException($"任务类型错误或缺少参数！");
        var jobType = typeof(SqlSugarClient);
        // 传入执行程序集
        IJobDetail job = new JobDetailImpl(sysTasks.BaseId.ToString(), sysTasks.TaskGroup, jobType);
        return job;
    }

    /// <summary>
    /// 创建 Interval 类型的触发器
    /// 定时任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    private static ITrigger CreateIntervalTrigger(SysTasks sysTasks)
    {
        if (sysTasks.TriggerType != TriggerTypeEnum.Interval.GetEnumValueByKey())
            throw new AggregateException($"触发器类型错误或触发条件未通过验证！");
        // 设置开始时间和结束时间
        sysTasks.BeginTime ??= DateTime.Now;
        sysTasks.EndTime ??= DateTime.MaxValue.AddDays(-1);
        var starRunTime = DateBuilder.NextGivenSecondDate(sysTasks.BeginTime, 1);
        var endRunTime = DateBuilder.NextGivenSecondDate(sysTasks.EndTime, 1);

        if (sysTasks.EndTime <= DateTime.Now)
            throw new Exception($"结束时间小于当前时间计划将不会被执行！");
        if (sysTasks.CycleRunTimes != 0 && sysTasks.CycleHasRunTimes >= sysTasks.CycleRunTimes)
            throw new Exception($"该任务计划已完成:【{sysTasks.TaskName}】,无需重复启动,如需启动请修改已循环次数再提交");

        // 触发作业立即运行，然后每N秒重复一次，无限循环
        if (sysTasks.RunTimes > 0)
        {
            var trigger = TriggerBuilder.Create()
                .WithIdentity(sysTasks.BaseId.ToString(), sysTasks.TaskGroup)
                .StartAt(starRunTime)
                .EndAt(endRunTime)
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(sysTasks.IntervalSecond)
                    .WithRepeatCount(sysTasks.RunTimes))
                .ForJob(sysTasks.BaseId.ToString(), sysTasks.TaskGroup)
                .Build();
            return trigger;
        }
        else
        {
            var trigger = TriggerBuilder.Create()
                .WithIdentity(sysTasks.BaseId.ToString(), sysTasks.TaskGroup)
                .StartAt(starRunTime)
                .EndAt(endRunTime)
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(sysTasks.IntervalSecond)
                    .RepeatForever())
                .ForJob(sysTasks.BaseId.ToString(), sysTasks.TaskGroup)
                .Build();
            return trigger;
        }
    }

    /// <summary>
    /// 创建 Cron 类型的触发器
    /// 时间点或者周期性任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    private static ITrigger CreateCronTrigger(SysTasks sysTasks)
    {
        if (sysTasks.TriggerType != TriggerTypeEnum.Cron.GetEnumValueByKey())
            throw new AggregateException($"触发器类型错误或触发条件未通过验证！");
        // 设置开始时间和结束时间
        sysTasks.BeginTime ??= DateTime.Now;
        sysTasks.EndTime ??= DateTime.MaxValue.AddDays(-1);
        var starRunTime = DateBuilder.NextGivenSecondDate(sysTasks.BeginTime, 1);
        var endRunTime = DateBuilder.NextGivenSecondDate(sysTasks.EndTime, 1);

        if (sysTasks.Cron == null || !CronExpression.IsValidExpression(sysTasks.Cron))
            throw new AggregateException($"触发器类型错误或触发条件未通过验证！");
        // 作业触发器
        var trigger = TriggerBuilder.Create()
            .WithIdentity(sysTasks.BaseId.ToString(), sysTasks.TaskGroup)
            .StartAt(starRunTime)
            .EndAt(endRunTime)
            .WithCronSchedule(sysTasks.Cron)
            .ForJob(sysTasks.BaseId.ToString(), sysTasks.TaskGroup)
            .Build();
        // 解决 Quartz 启动后第一次会立即执行的办法
        ((CronTriggerImpl)trigger).MisfireInstruction = MisfireInstruction.CronTrigger.DoNothing;
        return trigger;
    }

    #endregion
}