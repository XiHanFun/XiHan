#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysBotMDto
// Guid:a1fe449c-532d-44f5-9a6d-3bb4c412d717
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/9/4 2:25:22
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHanCore.Services.Bases.Dtos;

namespace XiHanCore.Services.Syses.Bots.Dtos;

/// <summary>
/// SysBotMDto
/// </summary>
public class SysBotMDto : SysBotCDto, IBaseIdDto
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public long BaseId { get; set; }
}