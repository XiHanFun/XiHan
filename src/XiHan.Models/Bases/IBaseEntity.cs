#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IBaseEntity
// Guid:728eeb9a-6999-44ea-9882-bbddededb9a4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/14 4:28:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Models.Bases;

/// <summary>
/// 通用主键接口
/// </summary>
public interface IBaseIdEntity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    TKey BaseId { get; set; }
}

/// <summary>
/// 软删除接口过滤器
/// 只有实现了该接口的类才可以调用 Repository 的软删除方法
/// </summary>
public interface ISoftDeleteFilter
{
    /// <summary>
    /// 是否已删除
    /// </summary>
    bool IsDeleted { get; set; }
}

/// <summary>
/// 机构部门标识接口过滤器
/// </summary>
public interface IOrgIdFilter
{
    /// <summary>
    /// 机构部门标识
    /// </summary>
    long? OrgId { get; set; }
}

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

/// <summary>
/// 种子数据接口过滤器
/// </summary>
public interface ISeedDataFilter<TEntity> where TEntity : class, new()
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    IEnumerable<TEntity> HasData();
}