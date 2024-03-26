#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IMustHaveTenant
// Guid:c3214b81-e791-49ab-895f-3ff83e50c68d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 7:04:26
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Domain.Core.Models.Entities.Abstracts;

/// <summary>
/// 通用租户接口
/// </summary>
public interface IMustHaveTenant<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 租户标识
    /// </summary>
    TKey TenantId { get; }
}