#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ITenantIdFilter
// Guid:db0f9094-3a45-48c5-a093-226ef316dfa2
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:31:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Models.Bases.Filters;

/// <summary>
/// 租户接口过滤器
/// </summary>
public interface ITenantIdFilter
{
    /// <summary>
    /// 租户标识
    /// </summary>
    long? TenantId { get; set; }
}