#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IDependedTypesProvider
// Guid:88035bb9-3ada-4d5c-aadc-cfef76fcfddb
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 05:22:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;

namespace XiHan.Core.Modularity.Abstracts;

/// <summary>
/// 依赖类型提供器接口
/// </summary>
public interface IDependedTypesProvider
{
    /// <summary>
    /// 获取依赖类型
    /// </summary>
    /// <returns></returns>
    [NotNull]
    Type[] GetDependedTypes();
}