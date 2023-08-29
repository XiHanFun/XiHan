#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobIdDto
// Guid:114ae362-f805-4f00-9698-5eca0dbd902b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/30 4:51:24
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHan.Services.Bases.Dtos;

namespace XiHan.Services.Syses.Jobs.Dtos;

/// <summary>
/// SysJobIdDto
/// </summary>
public class SysJobIdDto : IBaseIdDto
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public long BaseId { get; set; }
}