#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:TaskSetup
// Guid:88fe88b0-4bc5-47f7-89a9-07a91fb46161
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-11 下午 02:20:46
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using XiHan.Tasks.QuartzNet.Bases;
using XiHan.Tasks.QuartzNet.Bases.Servers;

namespace XiHan.Application.Setups.Services;

/// <summary>
/// TaskSetup
/// </summary>
public static class TaskSetup
{
    /// <summary>
    /// 计划任务 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddTaskSchedulers(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 添加Quartz服务
        services.AddSingleton<IJobFactory, JobFactory>();
        services.AddSingleton<ITaskSchedulerServer, TaskSchedulerServer>();

        return services;
    }
}