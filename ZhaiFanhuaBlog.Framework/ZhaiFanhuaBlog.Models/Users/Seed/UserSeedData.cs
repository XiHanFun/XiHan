// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserSeedData
// Guid:af2431dd-8146-41b7-800b-1e174ec65d22
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-23 上午 10:49:27
// ----------------------------------------------------------------

using System.Text;
using ZhaiFanhuaBlog.Utils.Encryptions;

namespace ZhaiFanhuaBlog.Models.Users.Seed;

/// <summary>
/// UserSeedData
/// </summary>
public class UserSeedData
{
    /// <summary>
    /// 用户账户种子数据
    /// </summary>
    public static List<UserAccount> UserAccountList = new()
    {
        new UserAccount{
            UserName="administrator",
            UserEmail="administrator@example.com",
            Password=MD5Helper.EncryptMD5(Encoding.UTF8,"@Password12345678@"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="超级管理员",
            Signature="我是超级管理员",
            Gender=true
        },
        new UserAccount{
            UserName="admin",
            UserEmail="admin@example.com",
            Password=MD5Helper.EncryptMD5(Encoding.UTF8,"@Password12345678@"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="管理员",
            Signature="我是管理员",
            Gender=true
        },
        new UserAccount{
            UserName="user",
            UserEmail="user@example.com",
            Password=MD5Helper.EncryptMD5(Encoding.UTF8,"@Password12345678@"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="用户",
            Signature="我是用户",
            Gender=true
        },
        new UserAccount{
            UserName="test",
            UserEmail="test@example.com",
            Password=MD5Helper.EncryptMD5(Encoding.UTF8,"@Password12345678@"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="测试员",
            Signature="我是测试员",
            Gender=true
        },
    };
}