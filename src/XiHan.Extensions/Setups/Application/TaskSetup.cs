#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:TaskSetup
// Guid:b591b9ea-c246-4aab-b387-659f6cdf07d8
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-11 下午 02:22:59
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using XiHan.Models.Syses;
using XiHan.Tasks.Bases.Servers;
using XiHan.Utils.Console;

namespace XiHan.Extensions.Setups.Application;

/// <summary>
/// TaskSetup
/// </summary>
public static class TaskSetup
{
    /// <summary>
    /// 程序启动后添加任务计划
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static async Task<IApplicationBuilder> UseTaskSchedulersAsync(this IApplicationBuilder app)
    {
        ITaskSchedulerServer schedulerServer = app.ApplicationServices.GetRequiredService<ITaskSchedulerServer>();

        var tasks = await SqlSugar.IOC.DbScoped.SugarScope.Queryable<SysTasks>()
            .Where(m => m.IsStart)
            .ToListAsync();

        //程序启动后注册所有定时任务
        foreach (var task in tasks)
        {
            var result = await schedulerServer.AddTaskScheduleAsync(task);
            if (result.Success)
            {
                var info = $"注册任务：[{task.Name}]成功！";
                info.WriteLineSuccess();
                Log.Information(info);
            }
            else
            {
                var info = $"注册任务：[{task.Name}]失败！";
                info.WriteLineError();
                Log.Error(info);
            }
        }

        return app;
    }
}