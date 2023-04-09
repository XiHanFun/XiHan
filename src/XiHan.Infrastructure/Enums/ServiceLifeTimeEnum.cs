#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ServiceLifeTimeEnum
// Guid:2a8fb64c-6038-4cde-8ebc-03976c2c23e2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-13 上午 01:18:02
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Infrastructure.Enums;

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