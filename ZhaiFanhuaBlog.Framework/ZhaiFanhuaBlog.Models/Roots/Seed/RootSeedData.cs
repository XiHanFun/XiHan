// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootSeedData
// Guid:93d1dcc9-a012-4025-92ee-217fb8713fa0
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-23 上午 10:49:27
// ----------------------------------------------------------------

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
            Name="读写",
            Type="数据管理权限",
            Description="这是用于用户浏览和编辑的权限",
        },
        new RootAuthority{
            Name="只读",
            Type="数据管理权限",
            Description="这是用于访客仅供浏览的权限",
        },
        // 功能操作权限
        new RootAuthority{
            Name="用户管理",
            Type="功能操作权限",
            Description="这是用于用户管理的功能权限",
        },
    };

    /// <summary>
    /// 系统角色种子数据
    /// </summary>
    public static List<RootRole> RootRoleList = new()
    {
        new RootRole{
            Name="超级管理员",
            Description="超级管理员角色",
        },
        new RootRole{
            Name="管理员",
            Description="管理员角色",
        },
        new RootRole{
            Name="普通用户",
            Description="普通系统角色",
        },
        new RootRole{
            Name="未分配",
            Description="未分配角色",
        },
    };
}