// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PageModel
// Guid:6ff31d71-5883-463b-ae8a-4cfd218b925a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-20 下午 08:39:31
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Models.Bases.Response.Model;

/// <summary>
/// 分页数据类
/// </summary>
public class PageModel
{
    /// <summary>
    /// 当前页标
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { set; get; }

    /// <summary>
    /// 总页数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 数据总数
    /// </summary>
    public int DataCount { get; set; }

    /// <summary>
    /// 数据集合
    /// </summary>
    public List<dynamic>? Data { get; set; }
}