#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserSeedData
// Guid:fa827fb4-76f2-457a-bb82-3a45da6fa400
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-04 下午 03:42:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Interface;
using XiHan.Utils.Encryptions;

namespace XiHan.Models.Syses.SeedData;

/// <summary>
/// SysUserSeedData
/// </summary>
public class SysUserSeedData : ISeedData<SysUser>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    [IgnoreUpdate]
    public IEnumerable<SysUser> HasData()
    {
        var encryptPassword = Md5EncryptionHelper.Encrypt(DesEncryptionHelper.Encrypt(AppGlobalConstant.DefaultPassword));
        return new List<SysUser>
        {
            new()
            {
                BaseId = 1,
                Account = "administrator",
                Password = encryptPassword,
                NickName = "超级管理员",
                RealName = "超级管理员",
                Email = "administrator@xihan.fun",
                RegisterFrom = "系统种子数据"
            },
            new()
            {
                BaseId = 2,
                Account = "admin",
                Password = encryptPassword,
                NickName = "管理员",
                RealName = "管理员",
                Email = "admin@xihan.fun",
                RegisterFrom = "系统种子数据"
            },
            new()
            {
                BaseId = 3,
                Account = "user",
                Password = encryptPassword,
                NickName = "普通用户",
                RealName = "普通用户",
                Email = "user@xihan.fun",
                RegisterFrom = "系统种子数据"
            },
            new()
            {
                BaseId = 4,
                Account = "test",
                Password = encryptPassword,
                NickName = "测试用户",
                RealName = "测试用户",
                Email = "test@xihan.fun",
                RegisterFrom = "系统种子数据"
            }
        };
    }
}