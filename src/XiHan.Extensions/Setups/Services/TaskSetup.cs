#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:TaskSetup
// Guid:88fe88b0-4bc5-47f7-89a9-07a91fb46161
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-11 下午 02:20:46
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using XiHan.Tasks.Bases;
using XiHan.Tasks.Bases.Servers;
using XiHan.Tasks.Bases.Servers.Impl;

namespace XiHan.Extensions.Setups.Services;

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
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // 添加Quartz服务
        services.AddSingleton<IJobFactory, JobFactory>();
        // 添加自身服务
        services.AddTransient<ITaskSchedulerServer, TaskSchedulerServer>();

        return services;
    }
}