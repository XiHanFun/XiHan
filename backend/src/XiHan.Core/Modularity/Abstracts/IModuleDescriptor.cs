#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IModuleDescriptor
// Guid:f55a9333-469a-4629-a50a-8873489708b7
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 05:14:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Reflection;

namespace XiHan.Core.Modularity.Abstracts;

/// <summary>
/// 模块描述器接口
/// </summary>
public interface IModuleDescriptor
{
    /// <summary>
    /// 模块类
    /// </summary>
    Type Type { get; }

    /// <summary>
    /// 模块的主程序集
    /// </summary>
    Assembly Assembly { get; }

    /// <summary>
    /// 模块的所有组件
    /// 包括在模块 Type 上使用 AdditionalAssemblyAttribute 属性标记的主程序集和其他已定义的程序集
    /// </summary>
    Assembly[] AllAssemblies { get; }

    /// <summary>
    /// 曦寒模块类的实例(单例)
    /// </summary>
    IModule Instance { get; }

    /// <summary>
    /// 该模块是否作为插件加载
    /// </summary>
    bool IsLoadedAsPlugIn { get; }

    /// <summary>
    /// 此模块所依赖的模块
    /// 一个模块可以通过<see cref="DependsOnAttribute"/>属性依赖于另一个模块
    /// </summary>
    IReadOnlyList<IModuleDescriptor> Dependencies { get; }
}