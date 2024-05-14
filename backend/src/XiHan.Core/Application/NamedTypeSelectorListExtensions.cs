#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:NamedTypeSelectorListExtensions
// Guid:4ff8063a-8f0e-4a3b-9f15-b516b5e07141
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/27 0:32:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.Verification;

namespace XiHan.Core.Application;

/// <summary>
/// 命名类型选择器列表扩展方法
/// </summary>
public static class NamedTypeSelectorListExtensions
{
    /// <summary>
    /// 将类型列表添加到该列表中
    /// </summary>
    /// <param name="list">命名类型选择器列表</param>
    /// <param name="name">一个任意但唯一的名称（可以稍后用于从列表中移除类型）</param>
    /// <param name="types"></param>
    public static void Add(this IList<NamedTypeSelector> list, string name, params Type[] types)
    {
        CheckHelper.NotNull(list, nameof(list));
        CheckHelper.NotNull(name, nameof(name));
        CheckHelper.NotNull(types, nameof(types));

        list.Add(new NamedTypeSelector(name, type => types.Any(type.IsAssignableFrom)));
    }
}