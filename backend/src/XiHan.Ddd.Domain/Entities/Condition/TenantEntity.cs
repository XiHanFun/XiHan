#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseTenantEntity
// Guid:005113f0-7b64-4a61-bcb9-1fac4b37ca38
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 7:10:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Ddd.Domain.Entities.Abstracts;

namespace XiHan.Ddd.Domain.Entities.Condition;

/// <summary>
/// 租户抽象类
/// </summary>
public class TenantEntity : IMustHaveTenant<long>
{
    /// <summary>
    /// 租户标识
    /// </summary>
    [SugarColumn(ColumnDescription = "租户标识", IsOnlyIgnoreUpdate = true)]
    public virtual long TenantId { get; private set; }
}