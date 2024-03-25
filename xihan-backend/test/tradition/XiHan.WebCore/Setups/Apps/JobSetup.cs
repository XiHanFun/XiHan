#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:JobSetup
// Guid:b591b9ea-c246-4aab-b387-659f6cdf07d8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-11 下午 02:22:59
// ----------------------------------------------------------------​

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using XiHan.Infrastructures.Apps;
using XiHan.Jobs.Bases.Servers;
using XiHan.Services.Syses.Jobs;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;

namespace XiHan.WebCore.Setups.Apps;

/// <summary>
/// JobSetup
/// </summary>
public static class JobSetup
{
    /// <summary>
    /// 计划任务
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="CustomException"></exception>
    public static IApplicationBuilder UseJobSetup(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var env = App.WebHostEnvironment;
        // 环境变量，生产环境
        if (env.IsProduction())
        {
            RegisterScheduledTasks();
        }

        return app;
    }

    /// <summary>
    /// 注册计划任务
    /// </summary>
    /// <exception cref="CustomException"></exception>
    private static void RegisterScheduledTasks()
    {
        try
        {
            "正在从配置中检测所有需要初始化启动的计划任务……".WriteLineInfo();
            var schedulerServer = App.GetRequiredService<ITaskSchedulerServer>();
            var sysJobService = App.GetRequiredService<ISysJobService>();
            var jobs = sysJobService.QueryAsync(m => m.IsStart).Result;

            // 程序启动后注册所有计划任务
            "计划任务正在注册……".WriteLineInfo();
            foreach (var job in jobs)
            {
                var result = schedulerServer.CreateTaskScheduleAsync(job);
                if (result.Result.IsSuccess)
                {
                    var info = $"任务【{job.Name}】注册成功！";
                    info.WriteLineSuccess();
                    Log.Information(info);
                }
                else
                {
                    var info = $"任务【{job.Name}】注册失败！";
                    info.WriteLineError();
                    Log.Error(info);
                }
            }

            "计划任务注册成功！".WriteLineSuccess();

            "所有计划任务初始化启动已完成！".WriteLineSuccess();
        }
        catch (Exception ex)
        {
            ex.ThrowAndConsoleError("注册计划任务出错！");
        }
    }
}