#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:TaskSetup
// Guid:b591b9ea-c246-4aab-b387-659f6cdf07d8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-11 下午 02:22:59
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using XiHan.Models.Syses;
using XiHan.Tasks.Bases.Servers;
using XiHan.Utils.Extensions;

namespace XiHan.Application.Setups.Apps;

/// <summary>
/// TaskSetup
/// </summary>
public static class TaskSetup
{
    /// <summary>
    /// 计划任务
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseTaskSchedulers(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        try
        {
            ITaskSchedulerServer schedulerServer = app.ApplicationServices.GetRequiredService<ITaskSchedulerServer>();

            var tasks = SqlSugar.IOC.DbScoped.SugarScope.Queryable<SysTasks>()
                .Where(m => m.IsStart)
                .ToList();

            // 程序启动后注册所有定时任务
            foreach (var task in tasks)
            {
                var result = schedulerServer.CreateTaskScheduleAsync(task).Result;
                if (result.IsSuccess)
                {
                    var info = $"注册任务：{task.Name}成功！";
                    info.WriteLineSuccess();
                    Log.Information(info);
                }
                else
                {
                    var info = $"注册任务：{task.Name}失败！";
                    info.WriteLineError();
                    Log.Error(info);
                }
            }
        }
        catch (Exception ex)
        {
            var errorInfo = @$"注册定时任务出错！";
            errorInfo.WriteLineError();
            Log.Error(ex, errorInfo);
        }

        return app;
    }
}