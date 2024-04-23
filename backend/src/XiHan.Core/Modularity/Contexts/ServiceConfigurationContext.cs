#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceConfigurationContext
// Guid:a6358e75-9d4b-4ec2-841e-65782e754b2a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/19 1:47:18
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Core.System.Collections.Generic.Extensions;
using XiHan.Core.Verification;

namespace XiHan.Core.Modularity.Contexts;

/// <summary>
/// 服务配置上下文
/// </summary>
public class ServiceConfigurationContext
{
    /// <summary>
    /// 服务
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// 服务存储器
    /// </summary>
    public IDictionary<string, object?> Items { get; }

    /// <summary>
    /// 过程中可以存储的任意命名对象，服务注册阶段并在模块之间共享
    /// 这是<see cref="Items"/> 字典的一种快捷用法
    /// 如果给定的键在<see cref="Items"/> 字典中没有找到，则返回null
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public object? this[string key]
    {
        get => Items.GetOrDefault(key);
        set => Items[key] = value;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="services"></param>
    public ServiceConfigurationContext([NotNull] IServiceCollection services)
    {
        Services = CheckHelper.NotNull(services, nameof(services));
        Items = new Dictionary<string, object?>();
    }
}