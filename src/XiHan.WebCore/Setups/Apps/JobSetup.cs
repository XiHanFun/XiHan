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
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using XiHan.Jobs.Bases.Servers;
using XiHan.Models.Syses;
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
    /// <param name="env"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="CustomException"></exception>
    public static IApplicationBuilder UseTaskSchedulers(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        // 环境变量，生产环境
        if (env.IsProduction())
        {
            try
            {
                var schedulerServer = app.ApplicationServices.GetRequiredService<ITaskSchedulerServer>();
                var context = SqlSugar.IOC.DbScoped.SugarScope;

                var jobs = context.Queryable<SysJob>().Where(m => m.IsStart).ToList();

                // 程序启动后注册所有定时任务
                foreach (var job in jobs)
                {
                    var result = schedulerServer.CreateTaskScheduleAsync(job);
                    if (result.Result.IsSuccess)
                    {
                        var info = $"注册任务：{job.Name}成功！";
                        info.WriteLineSuccess();
                        Log.Information(info);
                    }
                    else
                    {
                        var info = $"注册任务：{job.Name}失败！";
                        info.WriteLineError();
                        Log.Error(info);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException($"注册定时任务出错！", ex);
            }
        }

        return app;
    }
}