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
public class RootAnnouncement : BaseEntity
{
    /// <summary>
    /// 公告人
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 公告标题
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)")]
    public string? Title { get; set; } = null;

    /// <summary>
    /// 公告内容
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(4000)")]
    public string? TheContent { get; set; } = null;

    /// <summary>
    /// 公告链接
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", IsNullable = true)]
    public string? Url { get; set; } = null;

    /// <summary>
    /// 公告结束时间
    /// </summary>
    public DateTime? ShowEndTime { get; set; }
}