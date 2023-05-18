#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ITaskSchedulerServer
// Guid:63dbeee2-7cee-4ea7-9ed4-6e3552b6d8d4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-11 下午 03:04:38
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using XiHan.Common.Responses.Results;
using XiHan.Models.Syses;

namespace XiHan.Tasks.Bases.Servers;

/// <summary>
/// ITaskSchedulerServer
/// </summary>
public interface ITaskSchedulerServer
{
    /// <summary>
    /// 添加一个计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    Task<BaseResultDto> AddTaskScheduleAsync(SysTasks sysTasks);

    /// <summary>
    /// 更新计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    Task<BaseResultDto> UpdateTaskScheduleAsync(SysTasks sysTasks);

    /// <summary>
    /// 删除指定计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    Task<BaseResultDto> DeleteTaskScheduleAsync(SysTasks sysTasks);

    /// <summary>
    /// 开启计划任务
    /// </summary>
    /// <returns></returns>
    Task<BaseResultDto> StartTaskScheduleAsync();

    /// <summary>
    /// 停止计划任务
    /// </summary>
    /// <returns></returns>
    Task<BaseResultDto> StopTaskScheduleAsync();

    /// <summary>
    /// 立即运行指定计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    Task<BaseResultDto> RunTaskScheduleAsync(SysTasks sysTasks);

    /// <summary>
    /// 暂停指定的计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    Task<BaseResultDto> PauseTaskScheduleAsync(SysTasks sysTasks);

    /// <summary>
    /// 恢复指定计划任务
    /// </summary>
    /// <param name="sysTasks"></param>
    /// <returns></returns>
    Task<BaseResultDto> ResumeTaskScheduleAsync(SysTasks sysTasks);
}