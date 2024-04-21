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

using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using XiHan.Core.System.Collections.Generic;
using XiHan.Core.Verification;

namespace XiHan.Core.Modularity;

/// <summary>
///
/// </summary>
public class ServiceConfigurationContext
{
    /// <summary>
    ///
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    ///
    /// </summary>
    public IDictionary<string, object?> Items { get; }

    /// <summary>
    /// Gets/sets arbitrary named objects those can be stored during
    /// the service registration phase and shared between modules.
    /// This is a shortcut usage of the <see cref="Items"/> dictionary.
    /// Returns null if given key is not found in the <see cref="Items"/> dictionary.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public object? this[string key]
    {
        get => Items.GetOrDefault(key);
        set => Items[key] = value;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    public ServiceConfigurationContext([NotNull] IServiceCollection services)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(services));

        Services = CheckHelper.NotNull(services, nameof(services));
        Items = new Dictionary<string, object?>();
    }
}