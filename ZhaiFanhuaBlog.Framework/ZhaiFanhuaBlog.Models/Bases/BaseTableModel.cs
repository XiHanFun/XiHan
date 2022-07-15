// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseTableModel
// Guid:9d512d36-cd5c-4102-bed2-6457e9093085
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-20 下午 08:44:39
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Models.Bases;

/// <summary>
/// 表格数据(支持分页)
/// </summary>
public class BaseTableModel
{
    /// <summary>
    /// 数据总数
    /// </summary>
    public int DataCount { get; set; }

    /// <summary>
    /// 数据集合
    /// </summary>
    public List<dynamic>? Data { get; set; }
}