#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IXiHanApplicationWithExternalServiceProvider
// Guid:8767ed6f-442d-40d7-b9b3-fbd4d0d9d098
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 上午 11:12:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;

namespace XiHan.Core.Application.Abstracts;

/// <summary>
/// 具有外部服务的曦寒应用提供器接口
/// </summary>
public interface IXiHanApplicationWithExternalServiceProvider : IXiHanApplication
{
    /// <summary>
    /// 设置服务提供器，但不初始化模块
    /// </summary>
    void SetServiceProvider([NotNull] IServiceProvider serviceProvider);

    /// <summary>
    /// 设置服务提供器并初始化所有模块，异步
    /// 如果之前调用过 <see cref="SetServiceProvider"/>，则应将相同的 <paramref name="serviceProvider"/> 实例传递给此方法
    /// </summary>
    Task InitializeAsync([NotNull] IServiceProvider serviceProvider);

    /// <summary>
    /// 设置服务提供器并初始化所有模块
    /// 如果之前调用过 <see cref="SetServiceProvider"/>，则应将相同的 <paramref name="serviceProvider"/> 实例传递给此方法
    /// </summary>
    void Initialize([NotNull] IServiceProvider serviceProvider);
}