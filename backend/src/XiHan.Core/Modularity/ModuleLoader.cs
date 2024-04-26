#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ModuleLoader
// Guid:fb610d63-c073-4feb-9fac-acf6ae83a2ae
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 22:54:34
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using XiHan.Core.Application;
using XiHan.Core.Microsoft.Extensions.DependencyInjection;
using XiHan.Core.Modularity.Abstracts;
using XiHan.Core.Modularity.PlugIns;
using XiHan.Core.System.Collections.Generic.Extensions;
using XiHan.Core.Verification;

namespace XiHan.Core.Modularity;

/// <summary>
/// 模块加载器
/// </summary>
public class ModuleLoader : IModuleLoader
{
    /// <summary>
    /// 加载模块
    /// </summary>
    /// <param name="services"></param>
    /// <param name="startupModuleType"></param>
    /// <param name="plugInSources"></param>
    /// <returns></returns>
    public IModuleDescriptor[] LoadModules(
        IServiceCollection services,
        Type startupModuleType,
        PlugInSourceList plugInSources)
    {
        CheckHelper.NotNull(services, nameof(services));
        CheckHelper.NotNull(startupModuleType, nameof(startupModuleType));
        CheckHelper.NotNull(plugInSources, nameof(plugInSources));

        var modules = GetDescriptors(services, startupModuleType, plugInSources);

        modules = SortByDependency(modules, startupModuleType);

        return [.. modules];
    }

    /// <summary>
    /// 获取模块描述器列表
    /// </summary>
    /// <param name="services"></param>
    /// <param name="startupModuleType"></param>
    /// <param name="plugInSources"></param>
    /// <returns></returns>
    private List<IModuleDescriptor> GetDescriptors(
        IServiceCollection services,
        Type startupModuleType,
        PlugInSourceList plugInSources)
    {
        var modules = new List<ModuleDescriptor>();

        FillModules(modules, services, startupModuleType, plugInSources);
        SetDependencies(modules);

        return modules.Cast<IModuleDescriptor>().ToList();
    }

    /// <summary>
    /// 填充模块
    /// </summary>
    /// <param name="modules"></param>
    /// <param name="services"></param>
    /// <param name="startupModuleType"></param>
    /// <param name="plugInSources"></param>
    protected virtual void FillModules(
        List<ModuleDescriptor> modules,
        IServiceCollection services,
        Type startupModuleType,
        PlugInSourceList plugInSources)
    {
        var logger = services.GetInitLogger<XiHanApplicationBase>();

        // 所有从启动模块开始的模块
        foreach (var moduleType in XiHanModuleHelper.FindAllModuleTypes(startupModuleType, logger))
        {
            modules.Add(CreateModuleDescriptor(services, moduleType));
        }

        // 插件模块
        foreach (var moduleType in plugInSources.GetAllModules(logger))
        {
            if (modules.Any(m => m.Type == moduleType))
            {
                continue;
            }

            modules.Add(CreateModuleDescriptor(services, moduleType, isLoadedAsPlugIn: true));
        }
    }

    /// <summary>
    /// 设置依赖项
    /// </summary>
    /// <param name="modules"></param>
    protected virtual void SetDependencies(List<ModuleDescriptor> modules)
    {
        foreach (var module in modules)
        {
            SetDependencies(modules, module);
        }
    }

    /// <summary>
    /// 按依赖项排序
    /// </summary>
    /// <param name="modules"></param>
    /// <param name="startupModuleType"></param>
    /// <returns></returns>
    protected virtual List<IModuleDescriptor> SortByDependency(List<IModuleDescriptor> modules, Type startupModuleType)
    {
        var sortedModules = modules.SortByDependencies(m => m.Dependencies);
        sortedModules.MoveItem(m => m.Type == startupModuleType, modules.Count - 1);
        return sortedModules;
    }

    /// <summary>
    /// 创建模块描述器
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleType"></param>
    /// <param name="isLoadedAsPlugIn"></param>
    /// <returns></returns>
    protected virtual ModuleDescriptor CreateModuleDescriptor(IServiceCollection services, Type moduleType, bool isLoadedAsPlugIn = false)
    {
        return new ModuleDescriptor(moduleType, CreateAndRegisterModule(services, moduleType), isLoadedAsPlugIn);
    }

    /// <summary>
    /// 创建并注册模块
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleType"></param>
    /// <returns></returns>
    protected virtual IXiHanModule CreateAndRegisterModule(IServiceCollection services, Type moduleType)
    {
        var module = (IXiHanModule)Activator.CreateInstance(moduleType)!;
        services.AddSingleton(moduleType, module);
        return module;
    }

    /// <summary>
    /// 设置依赖项
    /// </summary>
    /// <param name="modules"></param>
    /// <param name="module"></param>
    /// <exception cref="Exception"></exception>
    protected virtual void SetDependencies(List<ModuleDescriptor> modules, ModuleDescriptor module)
    {
        foreach (var dependedModuleType in XiHanModuleHelper.FindDependedModuleTypes(module.Type))
        {
            var dependedModule = modules.FirstOrDefault(m => m.Type == dependedModuleType) ??
                throw new Exception($"在 {module.Type.AssemblyQualifiedName} 无法找到依赖的模块 {dependedModuleType.AssemblyQualifiedName}！");
            module.AddDependency(dependedModule);
        }
    }
}