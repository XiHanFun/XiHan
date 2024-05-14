#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:NamedTypeSelector
// Guid:00ce2ecd-2d7a-4bf6-9746-0b558def9675
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/27 0:29:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.Application;

/// <summary>
/// 命名类型选择器
/// </summary>
public class NamedTypeSelector
{
    /// <summary>
    /// 选择器名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 断言
    /// </summary>
    public Func<Type, bool> Predicate { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="predicate">Predicate</param>
    public NamedTypeSelector(string name, Func<Type, bool> predicate)
    {
        Name = name;
        Predicate = predicate;
    }
}