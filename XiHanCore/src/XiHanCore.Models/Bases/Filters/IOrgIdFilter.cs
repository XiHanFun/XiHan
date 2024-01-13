#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IOrgIdFilter
// Guid:69aa4300-716c-4525-ad9e-e523605e8722
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:31:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHanCore.Models.Bases.Filters;

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