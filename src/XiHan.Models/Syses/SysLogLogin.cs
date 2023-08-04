#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogLogin
// Guid:30e63ed1-9f89-4676-8aa6-9521f6ab3d6d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:11:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统登录日志表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable(TableName = "Sys_Log_Login")]
public class SysLogLogin : BaseCreateEntity
{
    /// <summary>
    /// 用户账号
    /// </summary>
    [SugarColumn(Length = 20)]
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    [SugarColumn(IsNullable = true, Length = 10)]
    public string? RealName { get; set; }

    /// <summary>
    /// 提示消息
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? Message { get; set; }

    /// <summary>
    /// 登录Ip
    /// </summary>
    [SugarColumn(Length = 20, IsNullable = true)]
    public string? LoginIp { get; set; }

    /// <summary>
    /// 登录地点
    ///</summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? Location { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = true)]
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? OS { get; set; }

    /// <summary>
    /// 代理信息
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? Agent { get; set; }

    /// <summary>
    /// 登录状态
    /// </summary>
    public bool Status { get; set; }
}