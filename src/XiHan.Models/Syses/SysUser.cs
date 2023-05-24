#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysUser
// long:5c92c656-8955-4343-8e6f-7ba028f1eab4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:30:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统用户表
/// </summary>
/// <remarks> 记录创建，修改，删除，审核，状态信息</remarks>
[SugarTable(TableName = "Sys_User")]
public class SysUser : BaseEntity
{
    /// <summary>
    /// 用户名称
    /// </summary>
    [SugarColumn(Length = 20)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮件
    /// </summary>
    [SugarColumn(Length = 50)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 手机号
    /// </summary>
    [SugarColumn(Length = 11)]
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// 用户来源
    /// </summary>
    [SugarColumn(Length = 50)]
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// 用户密码（MD5加密）
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 头像路径
    /// </summary>
    [SugarColumn(Length = 200)]
    public string AvatarPath { get; set; } = @"/Images/Accounts/Avatar/defult.png";

    /// <summary>
    /// 用户昵称
    /// </summary>
    [SugarColumn(Length = 20, IsNullable = true)]
    public string? NickName { get; set; }

    /// <summary>
    /// 用户签名
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? Signature { get; set; }

    /// <summary>
    /// 用户性别 男(true)女(false)
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public bool? Gender { get; set; }

    /// <summary>
    /// 用户地址
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? Address { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 注册Ip
    /// </summary>
    [SugarColumn(IsOnlyIgnoreUpdate = true, IsNullable = true)]
    public string? RegisterIp { get; set; }

    /// <summary>
    /// 最后登录Ip
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = true, IsNullable = true)]
    public string? LastLoginIp { get; set; }

    /// <summary>
    /// 最后登录时间
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = true, IsNullable = true)]
    public DateTime? LastLoginTime { get; set; }

    #region 其他字段

    /// <summary>
    /// 是否超级管理员
    /// </summary>
    /// <returns></returns>
    public bool IsAdmin()
    {
        return IsAdmin(BaseId);
    }

    /// <summary>
    /// 是否超级管理员
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static bool IsAdmin(long userId)
    {
        return userId == 00000001L;
    }

    /// <summary>
    /// 所属角色集合
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<SysRole> SysRoles { get; set; } = new List<SysRole>();

    /// <summary>
    /// 所属角色主键集合
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<long> SysRoleIds { get; set; } = new List<long>();

    #endregion
}