#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IBaseOrgIdEntity
// Guid:69aa4300-716c-4525-ad9e-e523605e8722
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:31:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Domain.Bases.Abstracts;

/// <summary>
/// 通用机构接口
/// </summary>
public interface IBaseOrgIdEntity<TKey>
{
    /// <summary>
    /// 机构标识
    /// </summary>
    TKey OrgId { get; set; }
}