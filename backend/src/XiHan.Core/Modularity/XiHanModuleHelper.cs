#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ModuleHelper
// Guid:c4a6b768-894e-4704-b49b-3be8c802443d
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 05:35:43
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Logging;
using System.Reflection;
using XiHan.Utils.Extensions.Collections.Generic;

namespace XiHan.Core.Modularity;

/// <summary>
/// 曦寒模块帮助类
/// </summary>
public static class XiHanModuleHelper
{
    /// <summary>
    /// 查找所有模块类型
    /// </summary>
    /// <param name="startupModuleType"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public static List<Type> FindAllModuleTypes(Type startupModuleType, ILogger? logger)
    {
        var moduleTypes = new List<Type>();
        logger?.Log(LogLevel.Information, "加载曦寒模块:");
        AddModuleAndDependenciesRecursively(moduleTypes, startupModuleType, logger);
        return moduleTypes;
    }

    /// <summary>
    /// 查找依赖模块类型
    /// </summary>
    /// <param name="moduleType"></param>
    /// <returns></returns>
    public static List<Type> FindDependedModuleTypes(Type moduleType)
    {
        XiHanModule.CheckXiHanModuleType(moduleType);

        var dependencies = new List<Type>();

        var dependencyDescriptors = moduleType.GetCustomAttributes()
            .OfType<IDependedTypesProvider>();

        foreach (var descriptor in dependencyDescriptors)
        {
            foreach (var dependedModuleType in descriptor.GetDependedTypes())
            {
                dependencies.AddIfNotContains(dependedModuleType);
            }
        }

        return dependencies;
    }

    /// <summary>
    /// 获取所有程序集
    /// </summary>
    /// <param name="moduleType"></param>
    /// <returns></returns>
    public static Assembly[] GetAllAssemblies(Type moduleType)
    {
        var assemblies = new List<Assembly>();

        var additionalAssemblyDescriptors = moduleType.GetCustomAttributes()
            .OfType<IAdditionalModuleAssemblyProvider>();

        foreach (var descriptor in additionalAssemblyDescriptors)
        {
            foreach (var assembly in descriptor.GetAssemblies())
            {
                assemblies.AddIfNotContains(assembly);
            }
        }

        assemblies.Add(moduleType.Assembly);

        return [.. assemblies];
    }

    /// <summary>
    /// 递归添加模块和依赖项
    /// </summary>
    /// <param name="moduleTypes"></param>
    /// <param name="moduleType"></param>
    /// <param name="logger"></param>
    /// <param name="depth"></param>
    private static void AddModuleAndDependenciesRecursively(List<Type> moduleTypes, Type moduleType, ILogger? logger, int depth = 0)
    {
        XiHanModule.CheckXiHanModuleType(moduleType);

        if (moduleTypes.Contains(moduleType))
        {
            return;
        }

        moduleTypes.Add(moduleType);
        logger?.Log(LogLevel.Information, "{new string(' ', {depth} * 2)}- {FullName}", depth, moduleType.FullName);

        foreach (var dependedModuleType in FindDependedModuleTypes(moduleType))
        {
            AddModuleAndDependenciesRecursively(moduleTypes, dependedModuleType, logger, depth + 1);
        }
    }
}