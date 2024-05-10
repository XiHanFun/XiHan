#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IHasAudited
// Guid:e6640ea7-3d9a-4f12-b8c9-5d0af9909414
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 22:09:09
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Ddd.Domain.Entities.Abstracts;

/// <summary>
/// 通用审核接口
/// </summary>
public interface IHasAudited<TKey>
{
    /// <summary>
    /// 审核用户主键
    /// </summary>
    TKey AuditedId { get; }

    /// <summary>
    /// 审核用户名称
    /// </summary>
    string? AuditedBy { get; }

    /// <summary>
    /// 审核时间
    /// </summary>
    DateTime? AuditedTime { get; }
}