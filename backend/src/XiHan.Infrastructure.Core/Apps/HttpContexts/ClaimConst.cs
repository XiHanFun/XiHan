#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ClaimConst
// Guid:3b00786b-fdd2-4826-9821-cbc9e086c963
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/29 4:26:41
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Core.Apps.HttpContexts;

/// <summary>
/// Claim 常量
/// </summary>
public static class ClaimConst
{
    /// <summary>
    /// 是否为超级管理员
    /// </summary>
    public const string IsSuperAdmin = "IsSuperAdmin";

    /// <summary>
    /// 用户标识
    /// </summary>
    public const string UserId = "UserId";

    /// <summary>
    /// 租户标识
    /// </summary>
    public const string TenantId = "TenantId";

    /// <summary>
    /// 组织机构标识
    /// </summary>
    public const string OrgId = "OrgId";

    /// <summary>
    /// 组织机构名称
    /// </summary>
    public const string OrgName = "OrgName";

    /// <summary>
    /// 组织机构类型
    /// </summary>
    public const string OrgType = "OrgType";

    /// <summary>
    /// 账号类型
    /// </summary>
    public const string AccountType = "AccountType";

    /// <summary>
    /// 账号
    /// </summary>
    public const string Account = "Account";

    /// <summary>
    /// 昵称
    /// </summary>
    public const string NickName = "NickName";

    /// <summary>
    /// 姓名
    /// </summary>
    public const string RealName = "RealName";

    /// <summary>
    /// 角色
    /// </summary>
    public const string UserRole = "UserRole";

    /// <summary>
    /// 登录模式PC、APP
    /// </summary>
    public const string LoginMode = "LoginMode";

    /// <summary>
    /// 颁发者
    /// </summary>
    public const string Issuer = "Issuer";

    /// <summary>
    /// 签收者
    /// </summary>
    public const string Audience = "Audience";

    /// <summary>
    /// Token 替换项
    /// </summary>
    public const string TokenReplace = "Bearer ";
}