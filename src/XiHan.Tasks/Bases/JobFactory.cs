#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:JobFactory
// Guid:39fa2376-1280-4912-8ded-4e6df638456e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-11 下午 03:57:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using XiHan.Infrastructures.Exceptions;

namespace XiHan.Tasks.Bases;

/// <summary>
/// JobFactory
/// </summary>
public class JobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造函数
    /// 注入反射获取依赖对象
    /// </summary>
    /// <param name="serviceProvider"></param>
    public JobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
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
            var serviceScope = _serviceProvider.CreateScope();
            var job = serviceScope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
            if (job == null)
                throw new InvalidOperationException("Job服务为空或获取失败！");
            else
                return job;
        }
        catch (Exception ex)
        {
            throw new CustomException("Job创建失败！", ex);
        }
    }

    /// <summary>
    /// 销毁(释放)
    /// </summary>
    /// <param name="job"></param>
    public void ReturnJob(IJob job)
    {
        if (job is IDisposable disposable) disposable.Dispose();
    }
}