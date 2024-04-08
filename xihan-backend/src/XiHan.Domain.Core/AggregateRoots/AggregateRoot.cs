#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AggregateRoot
// Guid:e7c0bce1-c5c4-456c-816a-d84ed63d3f45
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 22:17:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Domain.Core.AggregateRoots.Abstracts;
using XiHan.Domain.Core.Entities.Abstracts;
using XiHan.Domain.Core.Events;

namespace XiHan.Domain.Core.AggregateRoots;

/// <summary>
/// 聚合根
/// </summary>
public class AggregateRoot : BaseDomainEvents, IAggregateRoot, IEntity<long>,
    IHasCreated<long?>, IHasModified<long?>, IHasDeleted<long?>, ISoftDeleted<long?>, IHasAudited<long?>
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnDescription = "主键标识")]
    public virtual long BaseId { get; private set; }

    /// <summary>
    /// 新增用户主键
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, ColumnDescription = "新增用户主键")]
    public virtual long? CreatedId { get; private set; }

    /// <summary>
    /// 新增用户名称
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, Length = 32, ColumnDescription = "新增用户名称")]
    public virtual string? CreatedBy { get; private set; }

    /// <summary>
    /// 新增时间
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SplitField]
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, ColumnDescription = "新增时间")]
    public virtual DateTime CreatedTime { get; private set; } = DateTime.Now;

    /// <summary>
    /// 修改用户主键
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "修改用户主键")]
    public virtual long? ModifiedId { get; private set; }

    /// <summary>
    /// 修改用户名称
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, Length = 32, ColumnDescription = "修改用户名称")]
    public virtual string? ModifiedBy { get; private set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "修改时间")]
    public virtual DateTime? ModifiedTime { get; private set; }

    /// <summary>
    /// 是否已删除
    /// </summary>
    [SugarColumn(ColumnDescription = "是否已删除")]
    public virtual bool IsDeleted { get; private set; } = false;

    /// <summary>
    /// 删除用户主键
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "删除用户主键")]
    public virtual long? DeletedId { get; private set; }

    /// <summary>
    /// 删除用户名称
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, Length = 32, ColumnDescription = "删除用户名称")]
    public virtual string? DeletedBy { get; private set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "删除时间")]
    public virtual DateTime? DeletedTime { get; private set; }

    /// <summary>
    /// 审核用户主键
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "审核用户主键")]
    public virtual long? AuditedId { get; private set; }

    /// <summary>
    /// 审核用户名称
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, Length = 32, ColumnDescription = "审核用户名称")]
    public virtual string? AuditedBy { get; private set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "审核时间")]
    public virtual DateTime? AuditedTime { get; private set; }

    /// <summary>
    /// 判断主键是否为空，常用做判定操作是 新增 还是 编辑
    /// </summary>
    /// <returns></returns>
    public bool KeyIsNull()
    {
        return BaseId == 0;
    }

    /// <summary>
    /// 生成默认的主键值
    /// </summary>
    public void GenerateDefaultKeyVal()
    {
        if (BaseId == 0)
        {
        }
    }

    /// <summary>
    /// 逻辑删除
    /// </summary>
    /// <param name="deletedId"></param>
    /// <param name="deletedBy"></param>
    public virtual void SoftDelete(long? deletedId, string? deletedBy)
    {
        if (deletedId.HasValue && deletedBy is not null)
        {
            IsDeleted = true;
            DeletedId = deletedId;
            DeletedBy = deletedBy;
            DeletedTime = DateTime.Now;
        }
    }
}