#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OrderCondition
// Guid:d6dc72a4-bc53-4e45-91cf-ade09093c291
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 7:00:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Communication.Https.Enums;

namespace XiHan.Communication.Https.Entities;

/// <summary>
/// 通用排序条件
/// </summary>
public class OrderCondition
{
    /// <summary>
    /// 排序字段
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// 排序条件
    /// </summary>
    public OrderConditionEnum Condition { get; set; }

    /// <summary>
    /// 排序序号，按先后次序，层次依次升高
    /// </summary>
    public int SerialNumber { get; set; }
}