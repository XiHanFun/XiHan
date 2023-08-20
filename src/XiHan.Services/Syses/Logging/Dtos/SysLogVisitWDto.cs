#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysLogVisitWDto
// Guid:cb7aa82b-1b43-4b73-915c-54a05cde5530
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/21 1:59:51
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Services.Syses.Logging.Dtos;

/// <summary>
/// SysLogVisitWDto
/// </summary>
public class SysLogVisitWDto
{
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}