#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictDataWDto
// Guid:25256819-daec-401d-b76f-7047233afaca
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-13 上午 04:38:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHanCore.Services.Syses.Dicts.Dtos;

/// <summary>
/// SysDictDataWDto
/// </summary>
public class SysDictDataWDto
{
    /// <summary>
    /// 字典编码
    ///</summary>
    public string? TypeCode { get; set; }

    /// <summary>
    /// 字典项标签
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// 是否默认值
    /// </summary>
    public bool? IsDefault { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool? IsEnable { get; set; }
}