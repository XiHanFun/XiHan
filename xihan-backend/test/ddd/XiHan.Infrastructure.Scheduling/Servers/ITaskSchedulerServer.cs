#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ITaskSchedulerServer
// Guid:63dbeee2-7cee-4ea7-9ed4-6e3552b6d8d4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-11 下午 03:04:38
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using XiHan.Common.Shared.Https.Responses;
using XiHan.Common.Utilities.Exceptions;
using XiHan.Domain.Syses;
using XiHan.Infrastructure.Bases.Https.Responses;

namespace XiHan.Infrastructure.Scheduling.Servers;

/// <summary>
/// ITaskSchedulerServer
/// </summary>
public interface ITaskSchedulerServer
{
    /// <summary>
    /// 添加一个计划任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    Task<HttpResponseResult> CreateTaskScheduleAsync(SysJob sysJob);

    /// <summary>
    /// 删除指定计划任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    Task<HttpResponseResult> DeleteTaskScheduleAsync(SysJob sysJob);

    /// <summary>
    /// 更新计划任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    Task<HttpResponseResult> ModifyTaskScheduleAsync(SysJob sysJob);

    /// <summary>
    /// 开启计划任务
    /// </summary>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    Task<HttpResponseResult> StartTaskScheduleAsync();

    /// <summary>
    /// 停止计划任务
    /// </summary>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    Task<HttpResponseResult> StopTaskScheduleAsync();

    /// <summary>
    /// 立即运行指定计划任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    Task<HttpResponseResult> RunTaskScheduleAsync(SysJob sysJob);

    /// <summary>
    /// 暂停指定的计划任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    Task<HttpResponseResult> PauseTaskScheduleAsync(SysJob sysJob);

    /// <summary>
    /// 恢复指定计划任务
    /// </summary>
    /// <param name="sysJob"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    Task<HttpResponseResult> ResumeTaskScheduleAsync(SysJob sysJob);
}