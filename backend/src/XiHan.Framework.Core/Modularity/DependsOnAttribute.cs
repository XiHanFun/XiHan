#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DependsOnAttribute
// Guid:27fe20ca-388d-48e7-94b0-356b7da2488d
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 05:21:51
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.Modularity;

/// <summary>
/// 类型依赖特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependsOnAttribute : Attribute, IDependedTypesProvider
{
    /// <summary>
    /// 依赖类型集合
    /// </summary>
    public Type[] DependedTypes { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dependedTypes"></param>
    public DependsOnAttribute(params Type[]? dependedTypes)
    {
        DependedTypes = dependedTypes ?? Type.EmptyTypes;
    }

    /// <summary>
    /// 获取依赖类型
    /// </summary>
    /// <returns></returns>
    public virtual Type[] GetDependedTypes()
    {
        return DependedTypes;
    }
}