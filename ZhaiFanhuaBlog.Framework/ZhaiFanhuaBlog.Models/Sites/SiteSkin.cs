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
[SugarTable("SiteSkin", "网站皮肤表")]
public class SiteSkin : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 皮肤名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)", ColumnDescription = "皮肤名称")]
    public string SkinName { get; set; } = string.Empty;

    /// <summary>
    /// 皮肤路径
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", ColumnDescription = "皮肤路径")]
    public string SkinPath { get; set; } = string.Empty;
}