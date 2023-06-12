#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysDictTypeWhereDto
// Guid:3cd51a41-be16-4d82-901e-40e7dc59496b
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-06-12 下午 04:43:08
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Services.Syses.Dicts.Dtos;

/// <summary>
/// SysDictTypeWhereDto
/// </summary>
public class SysDictTypeWhereDto
{
    /// <summary>
    /// 字典名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 字典类型
    ///</summary>
    public string? Type { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool? IsEnable { get; set; }

    /// <summary>
    /// 是否系统内置
    /// </summary>
    public bool? IsOfficial { get; set; }
}