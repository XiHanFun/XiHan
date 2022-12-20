﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysSkin
// Guid:76cb14e8-a1a1-4f0b-a2b1-246d39879ade
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:40:24
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases.Entity;

namespace ZhaiFanhuaBlog.Models.Syses;

/// <summary>
/// 站点皮肤表
/// </summary>
public class SysSkin : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 皮肤名称
    /// </summary>
    [SugarColumn(Length =20)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 皮肤路径
    /// </summary>
    [SugarColumn(Length =200)]
    public string Path { get; set; } = string.Empty;
}