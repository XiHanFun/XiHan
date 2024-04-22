﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:XiHanApplicationCreationOptions
// Guid:b518acb4-c1e5-45f3-8451-c02da3d2babd
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 05:57:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using JetBrains.Annotations;
using XiHan.Core.Verification;
using XiHan.Core.Modularity.PlugIns;

namespace XiHan.Core.Options;

/// <summary>
/// 曦寒应用创建选项
/// </summary>
public class XiHanApplicationCreationOptions
{
    /// <summary>
    /// 服务容器
    /// </summary>
    [NotNull]
    public IServiceCollection Services { get; }

    /// <summary>
    /// 插件源列表
    /// </summary>
    [NotNull]
    public PlugInSourceList PlugInSources { get; }

    /// <summary>
    /// 此属性中的选项仅在未注册IConfiguration时生效。
    /// </summary>
    [NotNull]
    public XiHanConfigurationBuilderOptions Configuration { get; }

    public bool SkipConfigureServices { get; set; }

    public string? ApplicationName { get; set; }

    public string? Environment { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="services"></param>
    public XiHanApplicationCreationOptions([NotNull] IServiceCollection services)
    {
        Services = CheckHelper.NotNull(services, nameof(services));
        PlugInSources = [];
        Configuration = new();
    }
}