#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysDictDataWhereDto
// Guid:25256819-daec-401d-b76f-7047233afaca
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-13 上午 04:38:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Services.Syses.Dicts.Dtos;

/// <summary>
/// SysDictDataWhereDto
/// </summary>
public abstract class SysDictDataWhereDto
{
    /// <summary>
    /// 字典类型
    ///</summary>
    public string? Type { get; set; }

    /// <summary>
    /// 字典标签
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