// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CUserAccountRoleDto
// Guid:7d818509-753f-49a6-b6a9-21e7fd955915
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-03 下午 06:52:43
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.ViewModels.Users;

/// <summary>
/// CUserAccountRoleDto
/// </summary>
public class CUserAccountRoleDto
{
    /// <summary>
    /// 账户
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public Guid RoleId { get; set; }
}