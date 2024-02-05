#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IBaseTenantId
// Guid:db0f9094-3a45-48c5-a093-226ef316dfa2
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:31:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Domain.Bases.Entities.Interfaces;

/// <summary>
/// 通用租户接口
/// </summary>
public interface IBaseTenantId<TKey>
{
    /// <summary>
    /// 租户标识
    /// </summary>
    TKey TenantId { get; set; }
}