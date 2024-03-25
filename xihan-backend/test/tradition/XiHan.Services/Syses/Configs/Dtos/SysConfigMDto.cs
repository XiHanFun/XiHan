#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysConfigMDto
// Guid:d8e43aeb-69a6-4524-9f54-ee339c5cf24b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/18 22:22:32
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHan.Services.Bases.Dtos;

namespace XiHan.Services.Syses.Configs.Dtos;

/// <summary>
/// SysConfigMDto
/// </summary>
public class SysConfigMDto : SysConfigCDto, IBaseIdDto
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public long BaseId { get; set; }
}