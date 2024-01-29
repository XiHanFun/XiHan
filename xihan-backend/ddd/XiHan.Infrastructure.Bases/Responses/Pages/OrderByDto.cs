#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OrderByDto
// Guid:3b0d54b8-d305-4c58-b052-071a71f71f84
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/1/25 11:22:34
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Bases.Responses.Pages;

/// <summary>
/// OrderByDto
/// </summary>
public class OrderByDto
{
    /// <summary>
    /// 排序字段
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// 是否降序
    /// </summary>
    public bool IsDescending { get; set; }
}