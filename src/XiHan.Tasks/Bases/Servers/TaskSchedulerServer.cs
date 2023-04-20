#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:TaskSchedulerServer
// Guid:0415d360-cb23-4b9a-8f51-3636c2af2b72
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-11 下午 03:05:28
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;
using Quartz.Spi;
using Serilog;
using SqlSugar;
using System.Collections.Specialized;
using System.Reflection;
using XiHan.Infrastructure.Apps.Services;
using XiHan.Infrastructure.Contexts;
using XiHan.Infrastructure.Contexts.Results;
using XiHan.Models.Syses;
using XiHan.Models.Syses.Enums;
using XiHan.Utils.Consoles;
using XiHan.Utils.Enums;

namespace XiHan.Tasks.Bases.Servers;

/// <summary>
/// 任务调度管理中心
/// </summary>
[AppService(ServiceType = typeof(ITaskSchedulerServer), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
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
    private IScheduler GetTaskSchedulerAsync()
    {
        if (_scheduler != null)
        {
            return _scheduler;
        }
        // 从Factory中获取_scheduler实例
        var collection = new NameValueCollection()
        {
            {"quartz.serializer.type","binary"}
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
    public async Task<BaseResultDto> AddTaskScheduleAsync(SysTasks sysTasks)
    {
        try
        {
            var jobKey = new JobKey(sysTasks.Name, sysTasks.JobGroup);
            if (await _scheduler.CheckExists(jobKey))
            {
                return BaseResponseDto.BadRequest($"该计划任务已经在执行【{sysTasks.Name}】,请勿重复添加！");
            }

            // 判断触发器类型，并创建一个触发器
            ITrigger trigger = TriggerBuilder.Create().Build();
            switch (sysTasks.TriggerType)
            {
                // 定时任务
                case (int)TriggerTypeEnum.Interval:
                    trigger = CreateIntervalTrigger(sysTasks);
                    break;
                // 时间点或者周期性任务
                case (int)TriggerTypeEnum.Cron:
                    trigger = CreateCronTrigger(sysTasks);
                    break;
            }

            // 判断任务类型，并创建一个任务
            IJobDetail job = new JobDetailImpl();
            switch (sysTasks.JobType)
            {
                // 程序集
                case (int)JobTypeEnum.Assembly:
                    job = CreateAssemblyJobDetail(sysTasks);
                    break;
                // 网络请求
                case (int)JobTypeEnum.NetworkRequest:
                    job = CreateNetworkRequestJobDetail(sysTasks);
                    break;
                // SQL语句类型
                case (int)JobTypeEnum.SqlStatement:
                    job = CreateSqlStatementJobDetail(sysTasks);
                    break;
            }

            // 开启调度器，判断任务调度是否开启
            if (!_scheduler.IsStarted) await StartTaskScheduleAsync();

            // 将触发器和任务器绑定到调度器中
            await _scheduler.ScheduleJob(job, trigger);
            // 按新的trigger重新设置job执行
            await _scheduler.ResumeTrigger(trigger.Key);

            return BaseResponseDto.Ok($"启动计划任务【{sysTasks.Name}】成功！");
        }
        catch (Exception ex)
        {
            var errorInfo = $"启动计划任务【{sysTasks.Name}】失败！";
            Log.Error(ex, errorInfo);
            errorInfo.WriteLineError();
            return BaseResponseDto.InternalServerError(errorInfo);
        }
    }

    /// <summary>
    /// 更新计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> UpdateTaskScheduleAsync(SysTasks sysTasks)
    {
        try
        {
            var jobKey = new JobKey(sysTasks.Name, sysTasks.JobGroup);
            if (await _scheduler.CheckExists(jobKey))
            {
                // 防止创建时存在数据问题 先移除，然后在执行创建操作
                await _scheduler.DeleteJob(jobKey);
            }
            return BaseResponseDto.Ok($"修改计划【{sysTasks.Name}】成功！");
        }
        catch (Exception ex)
        {
            var errorInfo = $"修改计划任务【{sysTasks.Name}】失败！";
            Log.Error(ex, errorInfo);
            errorInfo.WriteLineError();
            return BaseResponseDto.InternalServerError(errorInfo);
        }
    }

    /// <summary>
    /// 删除指定计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DeleteTaskScheduleAsync(SysTasks sysTasks)
    {
        try
        {
            var jobKey = new JobKey(sysTasks.Name, sysTasks.JobGroup);
            await _scheduler.DeleteJob(jobKey);
            return BaseResponseDto.Ok($"删除计划任务【{sysTasks.Name}】成功！");
        }
        catch (Exception ex)
        {
            var errorInfo = $"删除计划任务【{sysTasks.Name}】失败！";
            Log.Error(ex, errorInfo);
            errorInfo.WriteLineError();
            return BaseResponseDto.InternalServerError(errorInfo);
        }
    }

    /// <summary>
    /// 开启计划任务
    /// </summary>
    /// <returns></returns>
    public async Task<BaseResultDto> StartTaskScheduleAsync()
    {
        try
        {
            _scheduler.JobFactory = _jobFactory;
            // 计划任务已经开启
            if (_scheduler.IsStarted)
            {
                return BaseResponseDto.Continue();
            }

            // 等待任务运行完成
            await _scheduler.Start();
            return BaseResponseDto.Ok("计划任务开启成功！");
        }
        catch (Exception ex)
        {
            var errorInfo = $"开启计划任务失败！";
            Log.Error(ex, errorInfo);
            errorInfo.WriteLineError();
            return BaseResponseDto.InternalServerError(errorInfo);
        }
    }

    /// <summary>
    /// 停止计划任务
    /// </summary>
    /// <returns></returns>
    public async Task<BaseResultDto> StopTaskScheduleAsync()
    {
        try
        {
            _scheduler.JobFactory = _jobFactory;
            // 计划任务已经停止
            if (_scheduler.IsShutdown)
            {
                return BaseResponseDto.Continue();
            }
            // 等待任务运行停止
            await _scheduler.Shutdown();
            return BaseResponseDto.Ok($"计划任务已经停止。");
        }
        catch (Exception ex)
        {
            var errorInfo = $"停止计划任务失败！";
            Log.Error(ex, errorInfo);
            errorInfo.WriteLineError();
            return BaseResponseDto.InternalServerError(errorInfo);
        }
    }

    /// <summary>
    /// 立即运行指定计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> RunTaskScheduleAsync(SysTasks sysTasks)
    {
        try
        {
            var jobKey = new JobKey(sysTasks.Name, sysTasks.JobGroup);
            var jobs = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(sysTasks.JobGroup));
            List<JobKey> jobKeys = jobs.ToList();
            if (jobKeys.Any())
            {
                await AddTaskScheduleAsync(sysTasks);
            }

            var triggers = await _scheduler.GetTriggersOfJob(jobKey);
            if (triggers.Count <= 0)
            {
                return BaseResponseDto.BadRequest($"未找到任务[{jobKey.Name}]的触发器！");
            }

            await _scheduler.TriggerJob(jobKey);
            return BaseResponseDto.Ok($"计划任务[{jobKey.Name}]运行成功！");
        }
        catch (Exception ex)
        {
            var errorInfo = $"执行计划任务【{sysTasks.Name}】失败";
            Log.Error(ex, errorInfo);
            errorInfo.WriteLineError();
            return BaseResponseDto.InternalServerError(errorInfo);
        }
    }

    /// <summary>
    /// 暂停指定的计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> PauseTaskScheduleAsync(SysTasks sysTasks)
    {
        try
        {
            var jobKey = new JobKey(sysTasks.Name, sysTasks.JobGroup);
            if (await _scheduler.CheckExists(jobKey))
            {
                // 防止创建时存在数据问题 先移除，然后在执行创建操作
                await _scheduler.PauseJob(jobKey);
            }
            return BaseResponseDto.Ok($"暂停计划任务:【{sysTasks.Name}】成功！");
        }
        catch (Exception ex)
        {
            var errorInfo = $"暂停计划任务【{sysTasks.Name}】失败！";
            Log.Error(ex, errorInfo);
            errorInfo.WriteLineError();
            return BaseResponseDto.InternalServerError(errorInfo);
        }
    }

    /// <summary>
    /// 恢复指定计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> ResumeTaskScheduleAsync(SysTasks sysTasks)
    {
        try
        {
            var jobKey = new JobKey(sysTasks.Name, sysTasks.JobGroup);
            if (!await _scheduler.CheckExists(jobKey))
            {
                return BaseResponseDto.BadRequest($"未找到计划任务【{sysTasks.Name}】！");
            }
            await _scheduler.ResumeJob(jobKey);
            return BaseResponseDto.Ok($"恢复计划任务【{sysTasks.Name}】成功！");
        }
        catch (Exception ex)
        {
            var errorInfo = $"恢复计划任务【{sysTasks.Name}】失败！";
            Log.Error(ex, errorInfo);
            errorInfo.WriteLineError();
            return BaseResponseDto.InternalServerError(errorInfo);
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
        if (sysTasks.JobType == JobTypeEnum.Assembly.GetEnumValueByKey() && sysTasks.AssemblyName != null && sysTasks.ClassName != null)
        {
            Assembly assembly = Assembly.Load(new AssemblyName(sysTasks.AssemblyName));
            Type? jobType = assembly.GetType(sysTasks.AssemblyName + "." + sysTasks.ClassName) ?? throw new AggregateException($"未找到该类型的任务计划！");
            // 传入执行程序集
            IJobDetail job = new JobDetailImpl(sysTasks.Name, sysTasks.JobGroup, jobType);
            if (sysTasks.JobParams != null)
            {
                job.JobDataMap.Add("JobParam", sysTasks.JobParams);
            }
            return job;
        }
        throw new AggregateException($"任务类型错误或缺少参数！");
    }

    /// <summary>
    /// 创建任务
    /// 网络请求类型
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    private static IJobDetail CreateNetworkRequestJobDetail(SysTasks sysTasks)
    {
        if (sysTasks.JobType == JobTypeEnum.NetworkRequest.GetEnumValueByKey() && sysTasks.RequestMethod != null && sysTasks.ApiUrl != null)
        {
            Type? jobType = typeof(HttpClient);
            // 传入执行程序集
            IJobDetail job = new JobDetailImpl(sysTasks.Name, sysTasks.JobGroup, jobType);
            return job;
        }
        throw new AggregateException($"任务类型错误或缺少参数！");
    }

    /// <summary>
    /// 创建任务
    /// SQL 语句类型
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    private static IJobDetail CreateSqlStatementJobDetail(SysTasks sysTasks)
    {
        if (sysTasks.JobType == JobTypeEnum.SqlStatement.GetEnumValueByKey() && sysTasks.SqlText != null)
        {
            Type? jobType = typeof(SqlSugarClient);
            // 传入执行程序集
            IJobDetail job = new JobDetailImpl(sysTasks.Name, sysTasks.JobGroup, jobType);
            return job;
        }
        throw new AggregateException($"任务类型错误或缺少参数！");
    }

    /// <summary>
    /// 创建 Interval 类型的触发器
    /// 定时任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    private static ITrigger CreateIntervalTrigger(SysTasks sysTasks)
    {
        if (sysTasks.TriggerType == TriggerTypeEnum.Interval.GetEnumValueByKey())
        {
            // 设置开始时间和结束时间
            sysTasks.BeginTime ??= DateTime.Now;
            sysTasks.EndTime ??= DateTime.MaxValue.AddDays(-1);
            DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(sysTasks.BeginTime, 1);
            DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(sysTasks.EndTime, 1);

            if (sysTasks.EndTime <= DateTime.Now)
            {
                throw new Exception($"结束时间小于当前时间计划将不会被执行！");
            }
            if (sysTasks.CycleRunTimes != 0 && sysTasks.CycleHasRunTimes >= sysTasks.CycleRunTimes)
            {
                throw new Exception($"该任务计划已完成:【{sysTasks.Name}】,无需重复启动,如需启动请修改已循环次数再提交");
            }

            // 触发作业立即运行，然后每N秒重复一次，无限循环
            if (sysTasks.RunTimes > 0)
            {
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(sysTasks.Name, sysTasks.JobGroup)
                    .StartAt(starRunTime)
                    .EndAt(endRunTime)
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(sysTasks.IntervalSecond)
                    .WithRepeatCount(sysTasks.RunTimes))
                    .ForJob(sysTasks.Name, sysTasks.JobGroup)
                    .Build();
                return trigger;
            }
            else
            {
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(sysTasks.Name, sysTasks.JobGroup)
                    .StartAt(starRunTime)
                    .EndAt(endRunTime)
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(sysTasks.IntervalSecond)
                    .RepeatForever())
                    .ForJob(sysTasks.Name, sysTasks.JobGroup)
                    .Build();
                return trigger;
            }
        }
        throw new AggregateException($"触发器类型错误或触发条件未通过验证！");
    }

    /// <summary>
    /// 创建 Cron 类型的触发器
    /// 时间点或者周期性任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    private static ITrigger CreateCronTrigger(SysTasks sysTasks)
    {
        if (sysTasks.TriggerType == TriggerTypeEnum.Cron.GetEnumValueByKey())
        {
            // 设置开始时间和结束时间
            sysTasks.BeginTime ??= DateTime.Now;
            sysTasks.EndTime ??= DateTime.MaxValue.AddDays(-1);
            DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(sysTasks.BeginTime, 1);
            DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(sysTasks.EndTime, 1);

            if (sysTasks.Cron != null && CronExpression.IsValidExpression(sysTasks.Cron))
            {
                // 作业触发器
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(sysTasks.Name, sysTasks.JobGroup)
                    .StartAt(starRunTime)
                    .EndAt(endRunTime)
                    .WithCronSchedule(sysTasks.Cron)
                    .ForJob(sysTasks.Name, sysTasks.JobGroup)
                    .Build();
                // 解决 Quartz 启动后第一次会立即执行的办法
                ((CronTriggerImpl)trigger).MisfireInstruction = MisfireInstruction.CronTrigger.DoNothing;
                return trigger;
            }
        }
        throw new AggregateException($"触发器类型错误或触发条件未通过验证！");
    }

    #endregion
}