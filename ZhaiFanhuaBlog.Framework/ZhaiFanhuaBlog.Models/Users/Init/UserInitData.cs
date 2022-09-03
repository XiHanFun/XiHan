// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserInitData
// Guid:af2431dd-8146-41b7-800b-1e174ec65d22
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-23 上午 10:49:27
// ----------------------------------------------------------------

using System.Text;
using ZhaiFanhuaBlog.Utils.Encryptions;

namespace ZhaiFanhuaBlog.Models.Users.Init;

/// <summary>
/// UserInitData
/// </summary>
public class UserInitData
{
    /// <summary>
    /// 用户账户种子数据
    /// </summary>
    public static List<UserAccount> UserAccountList = new()
    {
        new UserAccount{
            Name="administrator",
            Email="administrator@example.com",
            Password=MD5Helper.EncryptMD5(Encoding.UTF8,"@Password12345678@"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="超级管理员",
            Signature="我是超级管理员",
            Gender=true
        },
        new UserAccount{
            Name="admin",
            Email="admin@example.com",
            Password=MD5Helper.EncryptMD5(Encoding.UTF8,"@Password12345678@"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="管理员",
            Signature="我是管理员",
            Gender=true
        },
        new UserAccount{
            Name="user",
            Email="user@example.com",
            Password=MD5Helper.EncryptMD5(Encoding.UTF8,"@Password12345678@"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="用户",
            Signature="我是用户",
            Gender=true
        },
        new UserAccount{
            Name="test",
            Email="test@example.com",
            Password=MD5Helper.EncryptMD5(Encoding.UTF8,"@Password12345678@"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="测试员",
            Signature="我是测试员",
            Gender=true
        },
    };
}