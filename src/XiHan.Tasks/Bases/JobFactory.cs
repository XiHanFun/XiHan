#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:JobFactory
// Guid:39fa2376-1280-4912-8ded-4e6df638456e
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-11 下午 03:57:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;
using XiHan.Utils.Consoles;

namespace XiHan.Tasks.Bases;

/// <summary>
/// JobFactory
/// </summary>
public class JobFactory : IJobFactory
{
    private readonly ILogger<JobFactory> _ILogger;
    private readonly IServiceProvider _IServiceProvider;

    /// <summary>
    /// 构造函数
    /// 注入反射获取依赖对象
    /// </summary>
    /// <param name="iLogger"></param>
    /// <param name="iServiceProvider"></param>
    public JobFactory(ILogger<JobFactory> iLogger, IServiceProvider iServiceProvider)
    {
        _ILogger = iLogger;
        _IServiceProvider = iServiceProvider;
    }

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="bundle"></param>
    /// <param name="scheduler"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        try
        {
            var serviceScope = _IServiceProvider.CreateScope();
            var job = serviceScope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
            if (job == null)
            {
                throw new InvalidOperationException("Job服务为空或获取失败！");
            }
            else
            {
                return job;
            }
        }
        catch (Exception ex)
        {
            var errorInfo = "Job创建失败！";
            errorInfo.WriteLineError();
            _ILogger.LogError(ex, errorInfo);
            throw;
        }
    }

    /// <summary>
    /// 销毁(释放)
    /// </summary>
    /// <param name="job"></param>
    public void ReturnJob(IJob job)
    {
        var disposable = job as IDisposable;
        if (disposable != null)
        {
            disposable.Dispose();
        }
    }
}