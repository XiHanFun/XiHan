#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseTableDto
// Guid:9d512d36-cd5c-4102-bed2-6457e9093085
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-20 下午 08:44:39
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Contexts.Response.Tables;

/// <summary>
/// 通用表格实体(支持分页)
/// </summary>
public class BaseTableDto<TEntity> where TEntity : class
{
    /// <summary>
    /// 数据总数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 数据集合
    /// </summary>
    public List<TEntity>? Data { get; set; }
}