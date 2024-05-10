#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceLifeTimeEnum
// Guid:fee194e7-2ba9-4539-9326-c201ad8093b1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 14:32:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Infrastructure.Core.Apps.Services;

/// <summary>
/// 服务生命周期
/// </summary>
public enum ServiceLifeTimeEnum
{
    /// <summary>
    /// 单例
    /// </summary>
    [Description("单例")]
    Singleton,

    /// <summary>
    /// 作用域
    /// </summary>
    [Description("作用域")]
    Scoped,

    /// <summary>
    /// 瞬时
    /// </summary>
    [Description("瞬时")]
    Transient
}