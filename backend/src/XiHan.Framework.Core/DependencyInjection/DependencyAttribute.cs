#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DependencyAttribute
// Guid:02e153f7-890e-40e1-8779-a25b47c5e46b
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 05:10:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;

namespace XiHan.Framework.Core.DependencyInjection;

/// <summary>
/// 依赖特性
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class DependencyAttribute : Attribute
{
    /// <summary>
    /// 生命周期
    /// </summary>
    public virtual ServiceLifetime? Lifetime { get; set; }

    /// <summary>
    /// 是否尝试注册
    /// </summary>
    public virtual bool TryRegister { get; set; }

    /// <summary>
    /// 是否替换服务
    /// </summary>
    public virtual bool ReplaceServices { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public DependencyAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="lifetime"></param>
    public DependencyAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }
}