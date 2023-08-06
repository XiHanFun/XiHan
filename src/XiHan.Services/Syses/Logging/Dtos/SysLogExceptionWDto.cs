#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogExceptionWDto.cs
// Guid:3fcd7683-0372-44e9-a5be-273ae32aa0e7
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-21 下午 05:35:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Services.Syses.Logging.Dtos;

/// <summary>
/// SysLogExceptionWDto.cs
/// </summary>
public class SysLogExceptionWDto
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
    /// 日志级别
    /// </summary>
    public string? Level { get; set; }
}