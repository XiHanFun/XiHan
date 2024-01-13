#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysEmailLogWDto
// Guid:c36cef24-1621-460e-8e9f-63a6d20991bb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/31 7:03:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHanCore.Services.Syses.Emails.Dtos;

/// <summary>
/// SysEmailLogWDto
/// </summary>
public class SysEmailLogWDto
{
    /// <summary>
    /// 手机号码
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 是否已发送(1已发送 0未发送)
    /// </summary>
    public bool? IsSend { get; set; }

    /// <summary>
    /// 是否执行成功(1正常 0失败)
    /// </summary>
    public bool? IsSuccess { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}