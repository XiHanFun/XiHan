#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootAnnouncement
// Guid:0eeca4e2-11de-44f3-9c56-9205e87a9890
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:42:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases;

namespace XiHan.Models.Roots;

/// <summary>
/// 系统公告表
/// </summary>
[SugarTable(TableName = "RootAnnouncement")]
public class RootAnnouncement : BaseEntity
{
    /// <summary>
    /// 公告标题
    /// </summary>
    [SugarColumn(Length = 100)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 公告内容
    /// </summary>
    [SugarColumn(Length = 4000)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 公告链接
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? Url { get; set; }

    /// <summary>
    /// 公告结束时间
    /// </summary>
    public DateTime? ShowEndTime { get; set; }
}