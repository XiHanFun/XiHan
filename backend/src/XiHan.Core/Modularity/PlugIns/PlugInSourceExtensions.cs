#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PlugInSourceExtensions
// Guid:767c376e-94ab-424b-a594-725e03a44061
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/22 22:59:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using XiHan.Core.Verification;

namespace XiHan.Core.Modularity.PlugIns;

/// <summary>
/// 插件源扩展方法
/// </summary>
public static class PlugInSourceExtensions
{
    /// <summary>
    /// 获取所有模块和依赖项
    /// </summary>
    /// <param name="plugInSource"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    [NotNull]
    public static Type[] GetModulesWithAllDependencies([NotNull] this IPlugInSource plugInSource, ILogger logger)
    {
        CheckHelper.NotNull(plugInSource, nameof(plugInSource));

        return plugInSource.GetModules()
            .SelectMany(type => XiHanModuleHelper.FindAllModuleTypes(type, logger))
            .Distinct().ToArray();
    }
}