// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserInitData
// Guid:af2431dd-8146-41b7-800b-1e174ec65d22
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-23 上午 10:49:27
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Utils.Encryptions;

namespace ZhaiFanhuaBlog.Models.Users.Init;

/// <summary>
/// UserInitData
/// </summary>
public class UserInitData
{
    public static List<UserAuthority> UserAuthorityList = new()
    {
        // 数据管理权限
        new UserAuthority{
            Name="读写",
            Type="数据管理权限",
            Description="这是用于用户浏览和编辑的权限",
        },
        new UserAuthority{
            Name="只读",
            Type="数据管理权限",
            Description="这是用于访客仅供浏览的权限",
        },
        // 功能操作权限
        new UserAuthority{
            Name="用户管理",
            Type="功能操作权限",
            Description="这是用于用户管理的功能权限",
        },
    };

    public static List<UserRole> UserRoleList = new()
    {
        new UserRole{
            Name="超级管理员",
            Description="超级管理员角色",
        },
        new UserRole{
            Name="管理员",
            Description="管理员角色",
        },
        new UserRole{
            Name="普通用户",
            Description="普通用户角色",
        },
        new UserRole{
            Name="未分配",
            Description="未分配角色",
        },
    };

    public static List<UserAccount> UserAccountList = new()
    {
        new UserAccount{
            Name="administrator",
            Email=@"administrator@example.com",
            Password=MD5Helper.EncryptMD5("administrator"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="超级管理员",
            Signature="我是超级管理员",
            Gender=true
        },
        new UserAccount{
            Name="admin",
            Email=@"admin@example.com",
            Password=MD5Helper.EncryptMD5("admin"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="管理员",
            Signature="我是管理员",
            Gender=true
        },
        new UserAccount{
            Name="user",
            Email=@"user@example.com",
            Password=MD5Helper.EncryptMD5("admin"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="用户",
            Signature="我是用户",
            Gender=true
        },
        new UserAccount{
            Name="test",
            Email=@"test@example.com",
            Password=MD5Helper.EncryptMD5("admin"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="测试员",
            Signature="我是测试员",
            Gender=true
        },
    };
}