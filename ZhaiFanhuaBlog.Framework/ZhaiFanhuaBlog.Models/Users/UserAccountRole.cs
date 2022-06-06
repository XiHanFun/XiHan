// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAccountRole
// Guid:4bd03482-a95a-4c4e-a544-6e89ecf7c275
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 下午 05:14:15
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户账户角色关联表
/// </summary>
public class UserAccountRole : BaseEntity
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