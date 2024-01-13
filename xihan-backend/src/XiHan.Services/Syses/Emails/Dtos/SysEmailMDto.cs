#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysEmailMDto
// Guid:a1fe449c-532d-44f5-9a6d-3bb4c412d717
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/9/4 2:25:22
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHan.Services.Bases.Dtos;

namespace XiHan.Services.Syses.Emails.Dtos;

/// <summary>
/// SysEmailMDto
/// </summary>
public class SysEmailMDto : SysEmailCDto, IBaseIdDto
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public long BaseId { get; set; }
}