#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseEntity
// Guid:4d83ab9f-40ec-4d8e-b63e-db06932921fb
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:26:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Domain.Core.Entities.Abstracts;
using XiHan.Domain.Core.Events;

namespace XiHan.Domain.Core.Entities.Condition;

/// <summary>
/// 主键抽象类
/// </summary>
public abstract class IdEntity : BaseDomainEvents, IHasId<long>
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnDescription = "主键标识")]
    public virtual long BaseId { get; private set; }

    /// <summary>
    /// 判断主键是否为空，常用做判定操作是 新增 还是 编辑
    /// </summary>
    /// <returns></returns>
    public abstract bool KeyIsNull();

    /// <summary>
    /// 生成默认的主键值
    /// </summary>
    public abstract void GenerateDefaultKeyVal();

    #region 重写方法

    /// <summary>
    /// 重写方法，相等运算
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj is IdEntity entity)
        {
            return entity.BaseId == BaseId;
        }
        return false;
    }

    /// <summary>
    /// 重写方法，获取HashCode
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + GetType().GetHashCode();
            hash = hash * 23 + BaseId.GetHashCode();
            return hash;
        }
    }

    /// <summary>
    /// 重写方法，获取字符串
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{GetType().Name} [BaseId={BaseId}]";
    }

    #endregion
}