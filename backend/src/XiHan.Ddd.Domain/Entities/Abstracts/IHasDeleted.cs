#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IHasDeleted
// Guid:00a703ec-cff5-4253-9818-f44a02665ebc
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 21:58:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Ddd.Domain.Entities.Abstracts;

/// <summary>
/// 通用删除接口
/// </summary>
public interface IHasDeleted<TKey>
{
    /// <summary>
    /// 删除用户主键
    /// </summary>
    TKey DeletedId { get; }

    /// <summary>
    /// 删除用户名称
    /// </summary>
    string? DeletedBy { get; }

    /// <summary>
    /// 删除时间
    /// </summary>
    DateTime? DeletedTime { get; }
}