#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseOrgEntity
// Guid:3968e8ce-6ebb-4ea6-b16b-1b37181e16a7
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 7:10:43
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Domain.Core.Entities.Abstracts;

namespace XiHan.Domain.Core.Entities.Condition;

/// <summary>
/// 机构抽象类
/// </summary>
public abstract class OrgEntity : IMustHaveOrg<long>
{
    /// <summary>
    /// 机构标识
    /// </summary>
    [SugarColumn(ColumnDescription = "机构标识", IsOnlyIgnoreUpdate = true)]
    public virtual long OrgId { get; private set; }
}