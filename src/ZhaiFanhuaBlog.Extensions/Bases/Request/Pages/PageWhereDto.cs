#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PageWhereDto
// Guid:c5a5c87a-3140-4f8a-8ce6-4f953c4c1f61
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-16 上午 01:38:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Extensions.Bases.Response.Pages;

namespace ZhaiFanhuaBlog.Extensions.Bases.Request.Pages;

/// <summary>
/// 通用分页实体基类(不含条件)
/// </summary>
public class PageWhereDto
{
    /// <summary>
    /// 是否选择所有
    /// </summary>
    public bool SelectAll { get; set; }

    /// <summary>
    /// 排序列名
    /// </summary>
    public string? Sort { get; set; }

    /// <summary>
    /// 排序方式 默认 asc
    /// </summary>
    public string? Order { get; set; } = "asc";

    /// <summary>
    /// 分页实体
    /// </summary>
    public BasePageDto PageDto { get; set; } = new BasePageDto();
}

/// <summary>
/// 通用分页实体基类(包含条件)
/// </summary>
public class PageWhereDto<TEntity> : PageWhereDto where TEntity : class
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public TEntity? Where { get; set; }
}