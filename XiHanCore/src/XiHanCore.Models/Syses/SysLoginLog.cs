#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLoginLog
// Guid:30e63ed1-9f89-4676-8aa6-9521f6ab3d6d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:11:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Models.Bases.Attributes;
using XiHanCore.Models.Bases.Entities;

namespace XiHanCore.Models.Syses;

/// <summary>
/// 系统登录日志表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable, SystemTable]
public class SysLoginLog : BaseCreateEntity
{
    /// <summary>
    /// 登录是否成功
    /// </summary>
    [SugarColumn]
    public bool IsSuccess { get; set; }

    /// <summary>
    /// 用户账号
    /// </summary>
    [SugarColumn(Length = 32)]
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    [SugarColumn(Length = 32, IsNullable = true)]
    public string? RealName { get; set; }

    /// <summary>
    /// 提示消息
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 登录Ip
    /// </summary>
    [SugarColumn(Length = 64, IsNullable = true)]
    public string? Ip { get; set; }

    /// <summary>
    /// 登录地点
    ///</summary>
    [SugarColumn(Length = 64, IsNullable = true)]
    public string? Location { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    [SugarColumn(Length = 64, IsNullable = true)]
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    [SugarColumn(Length = 32, IsNullable = true)]
    public string? Os { get; set; }

    /// <summary>
    /// 代理信息
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Agent { get; set; }
}