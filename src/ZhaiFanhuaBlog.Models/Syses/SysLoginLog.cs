#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysLoginLog
// Guid:30e63ed1-9f89-4676-8aa6-9521f6ab3d6d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:11:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases.Entity;

namespace ZhaiFanhuaBlog.Models.Syses;

/// <summary>
/// 系统登录表
/// </summary>
public class SysLoginLog : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 登录状态
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// 提示消息
    /// </summary>
    [SugarColumn(Length =200, IsNullable = true)]
    public string? Message { get; set; }

    /// <summary>
    /// 登录Ip
    /// </summary>
    [SugarColumn(Length =20, IsNullable = true)]
    public string? LoginIp { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    [SugarColumn(Length =100, IsNullable = true)]
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统名称
    /// </summary>
    [SugarColumn(Length =50, IsNullable = true)]
    public string? OsName { get; set; }

    /// <summary>
    /// 代理信息
    /// </summary>
    [SugarColumn(Length =100, IsNullable = true)]
    public string? Agent { get; set; }
}