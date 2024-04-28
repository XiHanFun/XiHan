#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:XiHanModuleDescriptorExtensions
// Guid:d21eb93b-ed56-4611-8ee9-6fce9d47441b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/23 0:34:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Reflection;

namespace XiHan.Core.Modularity;

/// <summary>
/// 曦寒模块描述器扩展方法
/// </summary>
public static class XiHanModuleDescriptorExtensions
{
    /// <summary>
    /// 获取其他程序集
    /// </summary>
    /// <param name="module"></param>
    /// <returns></returns>
    public static Assembly[] GetAdditionalAssemblies(this IModuleDescriptor module)
    {
        return module.AllAssemblies.Length <= 1
            ? []
            : module.AllAssemblies.Where(x => x != module.Assembly).ToArray();
    }
}