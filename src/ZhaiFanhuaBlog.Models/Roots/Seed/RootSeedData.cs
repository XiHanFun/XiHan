#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootSeedData
// Guid:93d1dcc9-a012-4025-92ee-217fb8713fa0
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-23 上午 10:49:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace ZhaiFanhuaBlog.Models.Roots.Seed;

/// <summary>
/// RootSeedData
/// </summary>
public class RootSeedData
{
    /// <summary>
    /// 系统权限种子数据
    /// </summary>
    public static List<RootAuthority> RootAuthorityList = new()
    {
        // 数据管理权限
        new RootAuthority{
            AuthName="读写",
            AuthType="数据管理权限",
            Description="这是用于用户浏览和编辑的权限",
        },
        new RootAuthority{
            AuthName="只读",
            AuthType="数据管理权限",
            Description="这是用于访客仅供浏览的权限",
        },
        // 功能操作权限
        new RootAuthority{
            AuthName="用户管理",
            AuthType="功能操作权限",
            Description="这是用于用户管理的功能权限",
        },
    };

    /// <summary>
    /// 系统角色种子数据
    /// </summary>
    public static List<RootRole> RootRoleList = new()
    {
        new RootRole{
            RoleName="超级管理员",
            Description="超级管理员角色",
        },
        new RootRole{
            RoleName="管理员",
            Description="管理员角色",
        },
        new RootRole{
            RoleName="普通用户",
            Description="普通系统角色",
        },
        new RootRole{
            RoleName="未分配",
            Description="未分配角色",
        },
    };
}