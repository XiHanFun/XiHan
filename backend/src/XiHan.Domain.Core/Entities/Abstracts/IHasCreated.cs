#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IHasCreated
// Guid:031f703f-280d-4dbb-8b8d-b260ffd7b9a7
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 21:58:07
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Domain.Core.Entities.Abstracts;

/// <summary>
/// 通用新增接口
/// </summary>
public interface IHasCreated<TKey>
{
    /// <summary>
    /// 新增用户主键
    /// </summary>
    TKey CreatedId { get; }

    /// <summary>
    /// 新增用户名称
    /// </summary>
    string? CreatedBy { get; }

    /// <summary>
    /// 新增时间
    /// </summary>
    DateTime CreatedTime { get; }
}