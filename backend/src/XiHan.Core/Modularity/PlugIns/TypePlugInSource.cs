#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:TypePlugInSource
// Guid:32e3bd32-9ff7-4683-85cd-cc59ddc06351
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-28 上午 09:52:19
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;

namespace XiHan.Core.Modularity.PlugIns;

/// <summary>
/// 按类型加载插件源
/// </summary>
public class TypePlugInSource : IPlugInSource
{
    /// <summary>
    /// 模块类型
    /// </summary>
    private readonly Type[] _moduleTypes;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="moduleTypes"></param>
    public TypePlugInSource(params Type[]? moduleTypes)
    {
        _moduleTypes = moduleTypes ?? new Type[0];
    }

    /// <summary>
    /// 获取模块类型
    /// </summary>
    /// <returns></returns>
    [NotNull]
    public Type[] GetModules()
    {
        return _moduleTypes;
    }
}