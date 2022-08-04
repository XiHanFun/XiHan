// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogComment
// Guid:60383ed1-8cd3-43d1-85e8-8b3dc45cdc7e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:25:47
// ----------------------------------------------------------------

using SqlSugar;
using System.Net;
using ZhaiFanhuaBlog.Models.Bases;
using ZhaiFanhuaBlog.Utils.Formats;

namespace ZhaiFanhuaBlog.Models.Blogs;

/// <summary>
/// 文章评论表
/// </summary>
[SugarTable("BlogComment", "文章评论表")]
public class BlogComment : BaseEntity
{
    /// <summary>
    /// 评论者
    /// </summary>
    [SugarColumn(ColumnDescription = "文章总数")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 父级评论
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "父级评论")]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 所属文章
    /// </summary>
    [SugarColumn(ColumnDescription = "所属文章")]
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(4000)", ColumnDescription = "评论内容")]
    public string TheContent { get; set; } = string.Empty;

    /// <summary>
    /// 评论点赞数
    /// </summary>
    [SugarColumn(ColumnDescription = "评论点赞数")]
    public int PollCount { get; set; } = 0;

    /// <summary>
    /// 是否置顶 是(true)否(false)，只能置顶没有父级评论的项
    /// </summary>
    [SugarColumn(ColumnDescription = "是否置顶 是(true)否(false)，只能置顶没有父级评论的项")]
    public bool IsTop { get; set; } = false;

    /// <summary>
    /// 评论者IP(显示地区)
    /// </summary>
    [SugarColumn(ColumnDataType = "varbinary(16)", IsNullable = true, ColumnDescription = "评论者IP(显示地区)")]
    public virtual byte[]? CommentIp
    {
        get => IpFormatHelper.FormatIPAddressToByte(_CommentIp);
        set => _CommentIp = IpFormatHelper.FormatByteToIPAddress(value);
    }

    private IPAddress? _CommentIp;

    /// <summary>
    /// 代理信息
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", IsNullable = true, ColumnDescription = "代理信息")]
    public string? Agent { get; set; }
}