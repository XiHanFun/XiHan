#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISoftDeletedFilter
// Guid:62e03307-a4d3-47ea-8e0b-3bdd334fcdfe
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:30:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Domain.Bases.Filters;

/// <summary>
/// 软删除接口过滤器
/// 只有实现了该接口的类才可以调用 Repository 的软删除方法
/// </summary>
public interface ISoftDeletedFilter
{
    /// <summary>
    /// 是否已删除
    /// </summary>
    bool IsDeleted { get; set; }
}