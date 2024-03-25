#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysJobMDto
// Guid:7dfff8c1-1242-446a-8c2f-515f29d17f6a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/30 4:00:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHan.Services.Bases.Dtos;

namespace XiHan.Services.Syses.Jobs.Dtos;

/// <summary>
/// SysJobMDto
/// </summary>
public class SysJobMDto : SysJobCDto, IBaseIdDto
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public long BaseId { get; set; }
}