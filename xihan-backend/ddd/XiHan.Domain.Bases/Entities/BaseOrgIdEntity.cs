#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseOrgIdEntity
// Guid:6cac8147-babc-4326-a85b-eaf0fdddc6a9
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:29:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Domain.Bases.Abstracts;

namespace XiHan.Domain.Bases.Entities;

/// <summary>
/// 机构基类
/// </summary>
public abstract class BaseOrgIdEntity : IOrgIdEntity<long>
{
    /// <summary>
    /// 机构标识
    /// </summary>
    public virtual long OrgId { get; set; }
}