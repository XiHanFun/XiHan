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
    public static List<UserAccount> UserAccountList = new()
    {
        new UserAccount{
            Name="admin",
            Email=@"admin@example.com",
            Password=MD5Helper.EncryptMD5("admin"),
            AvatarPath= @"/Images/Accounts/Avatar/defult.png",
            NickName="管理员",
            Signature="",
            Gender=true,
            Address=""
        },
    };
}