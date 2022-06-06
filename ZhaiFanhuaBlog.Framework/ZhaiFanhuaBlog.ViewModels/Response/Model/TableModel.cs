// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TableModel
// Guid:9d512d36-cd5c-4102-bed2-6457e9093085
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-20 下午 08:44:39
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.ViewModels.Response.Enum;

namespace ZhaiFanhuaBlog.ViewModels.Response.Model;

/// <summary>
/// 表格数据(支持分页)
/// </summary>
public class TableModel<TEntity>
{
    /// <summary>
    /// 返回编码
    /// </summary>
    public ResultCode Code { get; set; }

    /// <summary>
    /// 返回信息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 数据总数
    /// </summary>
    public int DataCount { get; set; }

    /// <summary>
    /// 返回数据
    /// </summary>
    public List<TEntity>? Data { get; set; }
}