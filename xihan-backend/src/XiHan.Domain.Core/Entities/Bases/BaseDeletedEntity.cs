#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseDeletedEntity
// Guid:e47a0e96-1b19-4fc1-89d1-1c8834edd0cf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 7:06:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Domain.Core.Entities.Abstracts;

namespace XiHan.Domain.Core.Entities.Bases;

/// <summary>
/// 删除抽象类，含主键，新增，修改
/// </summary>
public abstract class BaseDeletedEntity : BaseModifiedEntity, IHasDeleted<long?>, ISoftDeleted<long?>
{
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