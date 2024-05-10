#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceCollectionLoggingExtensions
// Guid:77a56dcc-d9d3-493b-bd34-ed8d1504f923
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 22:59:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using XiHan.Framework.Core.Logging;

namespace XiHan.Framework.Core.Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 获取初始化日志
/// </summary>
public static class ServiceCollectionLoggingExtensions
{
    /// <summary>
    /// 获取初始化日志工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static ILogger<T> GetInitLogger<T>(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IInitLoggerFactory>().Create<T>();
    }
}