#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLoginLogWDto
// Guid:775abd3b-23a5-4c1d-bbc1-3692077b6869
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-19 下午 02:59:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Services.Syses.Logging.Dtos;

/// <summary>
/// SysLoginLogWDto
/// </summary>
public class SysLoginLogWDto
{
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 用户账号
    /// </summary>
    public string? Account { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    public string? RealName { get; set; } = string.Empty;

    /// <summary>
    /// 登录Ip
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// 登录是否成功
    /// </summary>
    public bool? IsSuccess { get; set; }
}