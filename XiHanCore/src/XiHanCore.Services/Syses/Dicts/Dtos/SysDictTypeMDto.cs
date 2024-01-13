#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictTypeMDto
// Guid:e13f22eb-50c1-40d8-921d-d895c806c605
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-18 下午 04:56:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHanCore.Services.Bases.Dtos;

namespace XiHanCore.Services.Syses.Dicts.Dtos;

/// <summary>
/// SysDictTypeMDto
/// </summary>
public class SysDictTypeMDto : SysDictTypeCDto, IBaseIdDto
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public long BaseId { get; set; }
}