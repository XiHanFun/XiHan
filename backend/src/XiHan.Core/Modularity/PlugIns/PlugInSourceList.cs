#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PlugInSourceList
// Guid:ae40351e-c619-423a-a6da-dc99459c80d8
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 06:04:39
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace XiHan.Core.Modularity.PlugIns;

/// <summary>
/// 插件源列表
/// </summary>
public class PlugInSourceList : List<IPlugInSource>
{
    /// <summary>
    /// 获取所有模块
    /// </summary>
    /// <param name="logger"></param>
    /// <returns></returns>
    [NotNull]
    internal Type[] GetAllModules(ILogger logger)
    {
        return this
            .SelectMany(pluginSource => pluginSource.GetModulesWithAllDependencies(logger))
            .Distinct()
            .ToArray();
    }
}