// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PageModel
// Guid:6ff31d71-5883-463b-ae8a-4cfd218b925a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-20 下午 08:39:31
// ----------------------------------------------------------------

using System.DirectoryServices.Protocols;

namespace ZhaiFanhuaBlog.ViewModels.Response.Model;

/// <summary>
/// 通用分页信息类
/// </summary>
public class PageModel<TEntity>
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 状态码
    /// </summary>
    public ResultCode Code { get; set; }

    /// <summary>
    /// 返回信息
    /// </summary>
    public string? Message { get; set; }

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
    /// 数据集合
    /// </summary>
    public List<TEntity>? Data { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; }
}