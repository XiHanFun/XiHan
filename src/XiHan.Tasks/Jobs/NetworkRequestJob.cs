#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:NetworkRequestJob
// Guid:2ab05c0c-71d1-4150-888e-0b788377944a
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-21 下午 04:38:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using Serilog;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Exceptions;
using XiHan.Infrastructures.Requests.Https;
using XiHan.Services.Syses.Tasks;
using XiHan.Tasks.Bases;
using XiHan.Utils.Extensions;

namespace XiHan.Tasks.Jobs;

/// <summary>
/// 网络请求任务
/// </summary>
[AppService(ServiceType = typeof(NetworkRequestJob), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class NetworkRequestJob : JobBase, IJob
{
    private static readonly ILogger _logger = Log.ForContext<SqlStatementJob>();

    private readonly IHttpPollyHelper _httpPollyHelper;
    private readonly ISysTasksService _sysTasksService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpPollyHelper"></param>
    /// <param name="sysTasksService"></param>
    public NetworkRequestJob(IHttpPollyHelper httpPollyHelper, ISysTasksService sysTasksService)
    {
        _httpPollyHelper = httpPollyHelper;
        _sysTasksService = sysTasksService;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        await base.ExecuteJob(context, async () => await NetworkRequest(context));
    }

    /// <summary>
    /// 网络请求
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task NetworkRequest(IJobExecutionContext context)
    {
        var result = string.Empty;
        if (context is JobExecutionContextImpl { Trigger: AbstractTrigger trigger })
        {
            var info = await _sysTasksService.GetByIdAsync(trigger.JobName);
            if (info == null)
                throw new CustomException($"网络请求任务【{trigger?.JobName}】执行失败，任务不存在！");

            var url = info.ApiUrl;
            var parms = info.JobParams;

            if (url.IsNullOrEmpty() && parms.IsNullOrEmpty())
                throw new CustomException($"网络请求任务【{trigger?.JobName}】执行失败，参数为空！");

            // POST请求
            if (info.RequestMethod != null && info.RequestMethod == RequestMethodEnum.POST.GetEnumValueByKey())
            {
                result = await _httpPollyHelper.PostAsync(HttpGroupEnum.Remote, url!, parms!);
            }
            // GET请求
            else
            {
                if (url!.IndexOf("?") > -1)
                {
                    url += "&" + parms;
                }
                else
                {
                    url += "?" + parms;
                }
                result = await _httpPollyHelper.GetAsync(HttpGroupEnum.Remote, url);
            }
            _logger.Information($"网络请求任务【{info.TaskName}】执行成功，请求结果为：" + result);
        }
    }
}