#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserWDto
// Guid:dd0da638-fc76-4803-80eb-d186c170333f
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-10 上午 04:07:19
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHanCore.Models.Syses.Enums;

namespace XiHanCore.Services.Syses.Users.Dtos;

/// <summary>
/// SysUserWDto
/// </summary>
public class SysUserWDto
{
    /// <summary>
    /// 用户账号
    /// </summary>
    public string? Account { get; set; }

    /// <summary>
    /// 用户昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string? RealName { get; set; }

    /// <summary>
    /// 用户性别 男(true)女(false)
    /// </summary>
    public GenderEnum? Gender { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    public string? Phone { get; set; }
}