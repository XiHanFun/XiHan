﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IApplicationInfoAccessor
// Guid:8402a6cb-08b3-4258-90f9-fe216920c89a
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 05:48:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;

namespace XiHan.Core.Abstracts;

/// <summary>
/// 应用信息访问器接口
/// </summary>
public interface IApplicationInfoAccessor
{
    /// <summary>
    /// 应用程序的名称
    /// 这对于有多个应用程序、应用程序资源位于一起的系统来说是很有用的
    /// </summary>
    string? ApplicationName { get; }

    /// <summary>
    /// 此应用程序实例的唯一标识符
    /// 当应用程序重新启动时，这个值会改变
    /// </summary>
    [NotNull]
    string InstanceId { get; }
}