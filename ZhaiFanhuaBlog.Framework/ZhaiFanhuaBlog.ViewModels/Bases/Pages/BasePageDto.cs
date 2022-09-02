// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BasePageDto
// Guid:1e6dfc38-1188-40e6-a6f0-5dee44d6209b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-02 上午 01:03:21
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.ViewModels.Bases.Pages;

/// <summary>
/// 通用分页实体基类
/// </summary>
public class BasePageDto
{
    /// <summary>
    /// 当前页标
    /// </summary>
    public int CurrentIndex { get; set; } = 1;

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { set; get; } = 20;
}

/// <summary>
/// 通用分页实体
/// </summary>
public class PageDto : BasePageDto
{
    /// <summary>
    /// 数据总数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 总页数
    /// </summary>
    public int PageCount { get; set; }
}

/// <summary>
/// 通用分页实体基类(不含条件)
/// </summary>
public class PageWhereDto
{
    /// <summary>
    /// 是否选择所有
    /// </summary>
    public bool SelectAll { get; set; } = false;

    /// <summary>
    /// 分页实体
    /// </summary>
    public BasePageDto PageDto { get; set; } = new BasePageDto();
}

/// <summary>
/// 通用分页实体基类(包含条件)
/// </summary>
public class PageWhereDto<Entity> : PageWhereDto where Entity : class
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public Entity? Where { get; set; }
}

/// <summary>
/// 通用分页数据实体
/// </summary>
public class PageDataDto<Entity> where Entity : class
{
    /// <summary>
    /// 分页数据
    /// </summary>
    public PageDto? Page { get; set; }

    /// <summary>
    /// 数据集合
    /// </summary>
    public List<Entity>? Datas { get; set; }
}