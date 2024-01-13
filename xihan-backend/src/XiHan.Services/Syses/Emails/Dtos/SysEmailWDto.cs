#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysEmailWDto
// Guid:9dd9ea5e-4b58-49d3-80bb-ba69c8c141dd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/9/4 2:30:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Services.Syses.Emails.Dtos;

/// <summary>
/// SysEmailWDto
/// </summary>
public class SysEmailWDto
{
    /// <summary>
    /// 标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool? IsEnabled { get; set; }
}