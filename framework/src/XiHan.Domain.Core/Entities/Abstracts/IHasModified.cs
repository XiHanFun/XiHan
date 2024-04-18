#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IHasModified
// Guid:50406f8d-2cc4-4a71-aad8-b0c8d850e7a3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 22:02:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Domain.Core.Entities.Abstracts;

/// <summary>
/// 通用修改接口
/// </summary>
public interface IHasModified<TKey>
{
    /// <summary>
    /// 修改用户主键
    /// </summary>
    TKey ModifiedId { get; }

    /// <summary>
    /// 修改用户名称
    /// </summary>
    string? ModifiedBy { get; }

    /// <summary>
    /// 修改时间
    /// </summary>
    DateTime? ModifiedTime { get; }
}