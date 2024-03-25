#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OrderConditionDto
// Guid:3b0d54b8-d305-4c58-b052-071a71f71f84
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/1/25 11:22:34
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Common.Shared.Pages.Enums;

namespace XiHan.Common.Shared.Pages.Dtos;

/// <summary>
/// 通用排序条件
/// </summary>
public class OrderConditionDto
{
    /// <summary>
    /// 排序字段
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// 排序条件
    /// </summary>
    public OrderConditionEnum OrderCondition { get; set; }

    /// <summary>
    /// 父级排序字段，按先后次序，层次依次升高
    /// </summary>
    public List<string> ParentFields { get; set; } = [];
}