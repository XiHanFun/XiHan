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

namespace XiHan.Infrastructures.Responses.Pages;

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
    /// 排序字段
    /// </summary>
    public string? OrderBy { get; set; }

    /// <summary>
    /// 是否默认排序方式  Asc
    /// </summary>
    public bool IsAsc { get; set; }

    /// <summary>
    /// 分页实体
    /// </summary>
    public PageDto Page { get; set; } = new();
}

/// <summary>
/// 通用分页实体基类(包含条件)
/// </summary>
public class PageWhereDto<TWhereEntity> : PageWhereDto where TWhereEntity : class
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public TWhereEntity Where { get; set; } = null!;
}