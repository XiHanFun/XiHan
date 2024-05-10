#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AssemblyHelper
// Guid:94b69b5b-42df-4236-9e8c-72cb536438b4
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 04:56:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Reflection;
using System.Runtime.Loader;

namespace XiHan.Framework.Core.Reflection;

/// <summary>
/// 程序集帮助类
/// </summary>
public static class AssemblyHelper
{
    /// <summary>
    /// 加载程序集
    /// </summary>
    /// <param name="folderPath"></param>
    /// <param name="searchOption"></param>
    /// <returns></returns>
    public static List<Assembly> LoadAssemblies(string folderPath, SearchOption searchOption)
    {
        return GetAssemblyFiles(folderPath, searchOption)
            .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
            .ToList();
    }

    /// <summary>
    /// 获取程序集文件
    /// </summary>
    /// <param name="folderPath"></param>
    /// <param name="searchOption"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetAssemblyFiles(string folderPath, SearchOption searchOption)
    {
        return Directory
            .EnumerateFiles(folderPath, "*.*", searchOption)
            .Where(s => s.EndsWith(".dll") || s.EndsWith(".exe"));
    }

    /// <summary>
    /// 获取所有类型
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IReadOnlyList<Type> GetAllTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            return ex.Types!;
        }
    }
}