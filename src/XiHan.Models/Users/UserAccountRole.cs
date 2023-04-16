﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAccountRole
// long:4bd03482-a95a-4c4e-a544-6e89ecf7c275
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 下午 05:14:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases;

namespace XiHan.Models.Users;

/// <summary>
/// 用户账户角色关联表
/// </summary>
[SugarTable(TableName = "UserAccountRole")]
public class UserAccountRole : BaseEntity
{
    /// <summary>
    /// 用户账户
    /// </summary>
    public long AccountId { get; set; }

    /// <summary>
    /// 系统角色
    /// </summary>
    public long RoleId { get; set; }
}