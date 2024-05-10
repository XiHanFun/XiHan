#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISoftDeleted
// Guid:5973b244-03b5-4614-98e4-2a8978f89022
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 7:07:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Ddd.Domain.Entities.Abstracts;

/// <summary>
/// 通用逻辑删除接口
/// </summary>
public interface ISoftDeleted<TKey>
{
    /// <summary>
    /// 是否已删除
    /// </summary>
    bool IsDeleted { get; }

    /// <summary>
    /// 逻辑删除
    /// </summary>
    /// <param name="deletedId"></param>
    /// <param name="deletedBy"></param>
    void SoftDelete(TKey deletedId, string? deletedBy);
}