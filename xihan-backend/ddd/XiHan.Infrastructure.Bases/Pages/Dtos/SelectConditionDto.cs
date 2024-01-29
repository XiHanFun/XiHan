#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SelectConditionDto
// Guid:20a53f95-527f-4f4f-80e5-81052263269e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/1/29 21:16:10
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructure.Bases.Pages.Enums;

namespace XiHan.Infrastructure.Bases.Pages.Dtos;

/// <summary>
/// 通用选择条件
/// </summary>
public class SelectConditionDto
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
    public SelectConditionEnum SelectCondition { get; set; }

    /// <summary>
    /// 父表字段集合，按先后次序，层次依次升高
    /// </summary>
    public List<string> ParentFields { get; set; } = [];
}