#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:TaskSchedulerService
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
using System.Collections.Specialized;
using System.Reflection;
using XiHan.Infrastructure.Contexts;
using XiHan.Infrastructure.Contexts.Results;
using XiHan.Models.Syses;
using XiHan.Utils.Consoles;

namespace XiHan.Tasks.Bases.Servers.Impl;

/// <summary>
/// 任务调度管理中心
/// </summary>
public class TaskSchedulerServer : ITaskSchedulerServer
{
    private readonly IScheduler Scheduler;
    private readonly IJobFactory JobFactory;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobFactory"></param>
    public TaskSchedulerServer(IJobFactory jobFactory)
    {
        Scheduler = GetTaskSchedulerAsync();
        JobFactory = jobFactory;
    }

    /// <summary>
    /// 获取计划任务
    /// </summary>
    /// <returns></returns>
    private IScheduler GetTaskSchedulerAsync()
    {
        if (Scheduler != null)
        {
            return Scheduler;
        }
        // 从Factory中获取Scheduler实例
        var collection = new NameValueCollection()
        {
            { "quartz.serializer.type","binary" },
        };
        var factory = new StdSchedulerFactory(collection);
        return factory.GetScheduler().Result;
    }

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
            if (await Scheduler.CheckExists(jobKey))
            {
                return BaseResponseDto.BadRequest($"该计划任务已经在执行【{sysTasks.Name}】,请勿重复添加！");
            }
            if (sysTasks.EndTime <= DateTime.Now)
            {
                return BaseResponseDto.BadRequest($"结束时间小于当前时间计划将不会被执行！");
            }

            // 1、设置开始时间和结束时间
            DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(sysTasks.BeginTime, 1);
            DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(sysTasks.EndTime, 1);

            // 2、开启调度器，判断任务调度是否开启
            if (!Scheduler.IsStarted) await StartTaskScheduleAsync();

            // 3、创建任务，传入反射出来的执行程序集
            Assembly assembly = Assembly.Load(new AssemblyName(sysTasks.AssemblyName));
            Type? jobType = assembly.GetType(sysTasks.AssemblyName + "." + sysTasks.ClassName);
            if (jobType == null) return BaseResponseDto.InternalServerError($"未找到该类型的任务计划！");
            IJobDetail job = new JobDetailImpl(sysTasks.Name, sysTasks.JobGroup, jobType);
            if (sysTasks.JobParams != null)
            {
                job.JobDataMap.Add("JobParam", sysTasks.JobParams);
            }

            //4、创建一个触发器
            ITrigger trigger;
            if (sysTasks.Cron != null && CronExpression.IsValidExpression(sysTasks.Cron))
            {
                trigger = CreateCronTrigger(sysTasks);
                // 解决Quartz启动后第一次会立即执行问题解决办法
                ((CronTriggerImpl)trigger).MisfireInstruction = MisfireInstruction.CronTrigger.DoNothing;
            }
            else
            {
                trigger = CreateSimpleTrigger(sysTasks);
                ((SimpleTriggerImpl)trigger).MisfireInstruction = MisfireInstruction.CronTrigger.DoNothing;
            }

            // 5、将触发器和任务器绑定到调度器中
            await Scheduler.ScheduleJob(job, trigger);
            //按新的trigger重新设置job执行
            await Scheduler.ResumeTrigger(trigger.Key);
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
            if (await Scheduler.CheckExists(jobKey))
            {
                // 防止创建时存在数据问题 先移除，然后在执行创建操作
                await Scheduler.DeleteJob(jobKey);
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
            await Scheduler.DeleteJob(jobKey);
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
            Scheduler.JobFactory = JobFactory;
            // 计划任务已经开启
            if (Scheduler.IsStarted)
            {
                return BaseResponseDto.Continue();
            }

            // 等待任务运行完成
            await Scheduler.Start();
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
            Scheduler.JobFactory = JobFactory;
            // 计划任务已经停止
            if (Scheduler.IsShutdown)
            {
                return BaseResponseDto.Continue();
            }
            // 等待任务运行停止
            await Scheduler.Shutdown();
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
            var jobs = await Scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(sysTasks.JobGroup));
            List<JobKey> jobKeys = jobs.ToList();
            if (jobKeys.Any())
            {
                await AddTaskScheduleAsync(sysTasks);
            }

            var triggers = await Scheduler.GetTriggersOfJob(jobKey);
            if (triggers.Count <= 0)
            {
                return BaseResponseDto.BadRequest($"未找到任务[{jobKey.Name}]的触发器！");
            }

            await Scheduler.TriggerJob(jobKey);
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
            if (await Scheduler.CheckExists(jobKey))
            {
                // 防止创建时存在数据问题 先移除，然后在执行创建操作
                await Scheduler.PauseJob(jobKey);
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
            if (!await Scheduler.CheckExists(jobKey))
            {
                return BaseResponseDto.BadRequest($"未找到计划任务【{sysTasks.Name}】！");
            }
            await Scheduler.ResumeJob(jobKey);
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

    /// <summary>
    /// 创建SimpleTrigger触发器（简单触发器）
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    private static ITrigger CreateSimpleTrigger(SysTasks sysTasks)
    {
        // 触发作业立即运行，然后每10秒重复一次，无限循环
        if (sysTasks.RunTimes > 0)
        {
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity(sysTasks.Name, sysTasks.JobGroup)
            .StartAt(sysTasks.BeginTime)
            .EndAt(sysTasks.EndTime)
            .WithSimpleSchedule(x => x.WithIntervalInSeconds(sysTasks.IntervalSecond)
            .WithRepeatCount(sysTasks.RunTimes)).ForJob(sysTasks.Name, sysTasks.JobGroup).Build();
            return trigger;
        }
        else
        {
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity(sysTasks.Name, sysTasks.JobGroup)
            .StartAt(sysTasks.BeginTime)
            .EndAt(sysTasks.EndTime)
            .WithSimpleSchedule(x => x.WithIntervalInSeconds(sysTasks.IntervalSecond)
            .RepeatForever()).ForJob(sysTasks.Name, sysTasks.JobGroup).Build();
            return trigger;
        }
    }

    /// <summary>
    /// 创建类型Cron的触发器
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    private static ITrigger CreateCronTrigger(SysTasks sysTasks)
    {
        // 作业触发器
        ITrigger trigger = TriggerBuilder.Create()
               .WithIdentity(sysTasks.Name, sysTasks.JobGroup)
               .StartAt(sysTasks.BeginTime)
               .EndAt(sysTasks.EndTime)
               .WithCronSchedule(sysTasks.Cron)
               .ForJob(sysTasks.Name, sysTasks.JobGroup)
               .Build();
        return trigger;
    }
}