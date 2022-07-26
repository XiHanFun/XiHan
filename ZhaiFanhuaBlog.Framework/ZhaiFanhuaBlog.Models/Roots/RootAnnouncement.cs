// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootAnnouncement
// Guid:0eeca4e2-11de-44f3-9c56-9205e87a9890
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:42:44
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Roots;

/// <summary>
/// 系统公告表
/// </summary>
[SugarTable("RootAnnouncement", "系统公告表")]
public class RootAnnouncement : BaseEntity
{
    /// <summary>
    /// 公告人
    /// </summary>
    [SugarColumn(ColumnDescription = "公告人")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 公告标题
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", ColumnDescription = "公告标题")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 公告内容
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(4000)", ColumnDescription = "公告内容")]
    public string TheContent { get; set; } = string.Empty;

    /// <summary>
    /// 公告链接
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", IsNullable = true, ColumnDescription = "公告链接")]
    public string? Url { get; set; }

    /// <summary>
    /// 公告结束时间
    /// </summary>
    [SugarColumn(ColumnDescription = "公告结束时间")]
    public DateTime? ShowEndTime { get; set; }
}