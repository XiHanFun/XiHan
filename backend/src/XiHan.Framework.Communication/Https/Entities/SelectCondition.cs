#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SelectCondition
// Guid:2de0fb9e-794a-4a1f-8ab0-551cb762957a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 7:07:33
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Framework.Communication.Https.Enums;

namespace XiHan.Framework.Communication.Https.Entities;

/// <summary>
/// 通用选择条件
/// </summary>
public class SelectCondition
{
    /// <summary>
    /// 选择字段
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// 字段值
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 值类型
    /// </summary>
    public string ValueType { get; set; } = string.Empty;

    /// <summary>
    /// 选择条件
    /// </summary>
    public SelectConditionEnum Condition { get; set; }
}