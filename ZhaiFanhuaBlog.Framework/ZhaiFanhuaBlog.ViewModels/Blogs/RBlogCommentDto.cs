// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RBlogCommentDto
// Guid:14b2d354-3e43-4164-882f-5331ac3c231b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-28 下午 10:36:37
// ----------------------------------------------------------------

using System.Net;
using ZhaiFanhuaBlog.Utils.Formats;
using ZhaiFanhuaBlog.ViewModels.Bases;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// RBlogCommentDto
/// </summary>
public class RBlogCommentDto : RBaseDto
{
    /// <summary>
    /// 评论者
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 父级评论
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 所属文章
    /// </summary>
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public string TheContent { get; set; } = string.Empty;

    /// <summary>
    /// 评论点赞数
    /// </summary>
    public int PollCount { get; set; } = 0;

    /// <summary>
    /// 是否置顶 是(true)否(false)，只能置顶没有父级评论的项
    /// </summary>
    public bool IsTop { get; set; } = false;

    /// <summary>
    /// 评论者IP(显示地区)
    /// </summary>
    public virtual byte[]? CommentIp
    {
        get => IpFormatHelper.FormatIPAddressToByte(_CommentIp);
        set => _CommentIp = IpFormatHelper.FormatByteToIPAddress(value);
    }

    private IPAddress? _CommentIp;
}