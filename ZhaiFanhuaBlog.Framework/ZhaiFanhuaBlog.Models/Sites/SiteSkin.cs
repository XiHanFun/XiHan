// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteSkin
// Guid:76cb14e8-a1a1-4f0b-a2b1-246d39879ade
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:40:24
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Sites;

/// <summary>
/// 网站皮肤表
/// </summary>
public class SiteSkin : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 皮肤名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 皮肤路径
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)")]
    public string Path { get; set; } = string.Empty;
}