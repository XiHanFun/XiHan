// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PageModel
// Guid:6ff31d71-5883-463b-ae8a-4cfd218b925a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-20 下午 08:39:31
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.ViewModels.Response.Model;

/// <summary>
/// 通用分页信息类
/// </summary>
public class PageModel<TEntity>
{
    /// <summary>
    /// 当前页标
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// 总页数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 数据总数
    /// </summary>
    public int DataCount { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { set; get; }

    /// <summary>
    /// 返回数据
    /// </summary>
    public List<TEntity>? Data { get; set; }
}