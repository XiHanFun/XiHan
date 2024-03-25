#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:UserAuthInfoDto
// Guid:d8e46605-e2da-4a1d-8e6a-b58b1f26bd5a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/29 2:00:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Claims;

namespace XiHan.Common.Shared.Https.Dtos;

/// <summary>
/// 权限信息
/// </summary>
public class UserAuthInfoDto
{
    /// <summary>
    /// 是否已鉴权
    /// </summary>
    public bool IsAuthenticated { get; set; }

    /// <summary>
    /// 是否为超级管理员
    /// </summary>
    public bool IsSuperAdmin { get; set; }

    /// <summary>
    /// 用户标识
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 租户标识
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 组织机构标识
    /// </summary>
    public long OrgId { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// 用户权限
    /// </summary>
    public string UserRole { get; set; } = string.Empty;

    /// <summary>
    /// 请求令牌
    /// </summary>
    public string UserToken { get; set; } = string.Empty;

    /// <summary>
    /// ClaimsIdentity
    /// </summary>
    public IEnumerable<ClaimsIdentity> UserClaims { get; set; } = [];
}