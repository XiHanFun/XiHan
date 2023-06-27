#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysUserWhereDto
// Guid:61d1cdf7-a47d-4ae2-b36b-969402c00901
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-27 上午 02:43:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Services.Syses.Users.Dtos;

/// <summary>
/// SysUserWhereDto
/// </summary>
public abstract class SysUserWhereDto
{
    /// <summary>
    /// 用户名称
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 电子邮件
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 用户昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// 用户性别 男(true)女(false)
    /// </summary>
    public bool? Gender { get; set; }
}