﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SqlStatementJob
// Guid:d0468458-6e00-4b87-ba7c-b521a933d904
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-21 下午 04:38:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using Serilog;
using SqlSugar.IOC;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Utils.Exceptions;
using XiHan.Jobs.Bases;
using XiHan.Services.Syses.Jobs;
using XiHan.Utils.Extensions;

namespace XiHan.Jobs.Jobs;

/// <summary>
/// 执行SQL任务
/// </summary>
[AppService(ServiceType = typeof(SqlStatementJob), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class SqlStatementJob : JobBase, IJob
{
    private static readonly ILogger _logger = Log.ForContext<SqlStatementJob>();
    private readonly ISysJobService _sysJobService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysJobService"></param>
    public SqlStatementJob(ISysJobService sysJobService)
    {
        _sysJobService = sysJobService;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        await base.ExecuteJob(context, async () => await SqlExecute(context));
    }

    /// <summary>
    /// 执行SQL
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task SqlExecute(IJobExecutionContext context)
    {
        if (context is JobExecutionContextImpl { Trigger: AbstractTrigger trigger })
        {
            var info = await _sysJobService.GetByIdAsync(trigger.JobName);

            if (info != null && info.SqlText.IsNotEmptyOrNull())
            {
                var result = DbScoped.SugarScope.Ado.ExecuteCommandWithGo(info.SqlText);
                _logger.Information($"执行SQL任务【{info.JobName}】执行成功，sql请求执行结果为：" + result);
            }
            else
            {
                throw new CustomException($"执行SQL任务【{trigger?.JobName}】执行失败，任务不存在！");
            }
        }
    }
}