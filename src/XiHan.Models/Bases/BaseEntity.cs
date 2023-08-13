#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseEntity
// Guid:84d15648-b4c6-40a5-8195-aae92765eb04
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 04:12:12
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace XiHan.Models.Bases;

/// <summary>
/// 主键基类
/// </summary>
public abstract class BaseIdEntity : IBaseIdEntity<long>
{
    /// <summary>
    /// 主键标识(雪花ID)
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnDescription = "主键标识")]
    public virtual long BaseId { get; set; }
}

/// <summary>
/// 新增基类，含主键
/// </summary>
public abstract class BaseCreateEntity : BaseIdEntity
{
    /// <summary>
    /// 新增用户主键
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, ColumnDescription = "新增用户主键")]
    public virtual long? CreatedId { get; set; }

    /// <summary>
    /// 新增用户名称
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, Length = 32, ColumnDescription = "新增用户名称")]
    public virtual string? CreatedBy { get; set; }

    /// <summary>
    /// 新增时间
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, ColumnDescription = "新增时间")]
    public virtual DateTime CreatedTime { get; set; } = DateTime.Now;
}

/// <summary>
/// 修改基类，含主键，新增
/// </summary>
public abstract class BaseModifyEntity : BaseCreateEntity
{
    /// <summary>
    /// 修改用户主键
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "修改用户主键")]
    public virtual long? ModifiedId { get; set; }

    /// <summary>
    /// 修改用户名称
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, Length = 32, ColumnDescription = "修改用户名称")]
    public virtual string? ModifiedBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "修改时间")]
    public virtual DateTime? ModifiedTime { get; set; } = DateTime.Now;
}

/// <summary>
/// 删除基类，含主键，新增，修改
/// </summary>
public abstract class BaseDeleteEntity : BaseModifyEntity, ISoftDeleteFilter
{
    /// <summary>
    /// 是否已删除
    /// </summary>
    [SugarColumn(ColumnDescription = "是否已删除")]
    public virtual bool IsDeleted { get; set; } = false;

    /// <summary>
    /// 删除用户主键
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "删除用户主键")]
    public virtual long? DeletedId { get; set; }

    /// <summary>
    /// 删除用户名称
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, Length = 32, ColumnDescription = "删除用户名称")]
    public virtual string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "删除时间")]
    public virtual DateTime? DeletedTime { get; set; }
}

/// <summary>
/// 审核基类，含主键，新增，修改，删除
/// </summary>
public abstract class BaseAuditEntity : BaseDeleteEntity
{
    /// <summary>
    /// 审核用户主键
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "审核用户主键")]
    public virtual long? AuditedId { get; set; }

    /// <summary>
    /// 审核用户名称
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, Length = 32, ColumnDescription = "审核用户名称")]
    public virtual string? AuditedBy { get; set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "审核时间")]
    public virtual DateTime? AuditedTime { get; set; }
}

/// <summary>
/// 状态基类，含主键，新增，修改，删除，审核
/// </summary>
public abstract class BaseStateEntity : BaseAuditEntity
{
    /// <summary>
    /// 状态项
    /// </summary>
    [SugarColumn(IsIgnore = true, Length = 64, ColumnDescription = "状态项")]
    public virtual string? StateKey { get; init; }

    /// <summary>
    /// 状态值
    /// </summary>
    [SugarColumn(IsIgnore = true, Length = 64, ColumnDescription = "状态值")]
    public virtual string? StateValue { get; init; }
}

/// <summary>
/// 实体基类，含主键，新增，修改，删除，审核，状态
/// </summary>
public abstract class BaseEntity : BaseStateEntity
{
}

/// <summary>
/// 机构部门实体基类
/// </summary>
public abstract class BaseOrgEntity : BaseEntity, IOrgIdFilter
{
    /// <summary>
    /// 机构部门标识
    /// </summary>
    public virtual long? OrgId { get; set; }
}

/// <summary>
/// 租户实体基类
/// </summary>
public class BaseTenantEntity : BaseEntity, ITenantIdFilter
{
    /// <summary>
    /// 租户标识
    /// </summary>
    [SugarColumn(ColumnDescription = "租户标识", IsOnlyIgnoreUpdate = true)]
    public virtual long? TenantId { get; set; }
}