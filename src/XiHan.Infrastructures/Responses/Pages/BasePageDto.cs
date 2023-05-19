#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BasePageDto
// Guid:ac953e11-5c32-44d5-a450-d377ef3a0453
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-04 下午 11:15:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructures.Responses.Pages;

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
/// 通用数据分页信息基类
/// </summary>
public class BasePageInfoDto : BasePageDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public BasePageInfoDto()
    { }

    /// <summary>
    /// 构造函数
    /// </summary>
    public BasePageInfoDto(int currentIndex, int pageSize, int totalCount)
    {
        CurrentIndex = currentIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        PageCount = (int)Math.Ceiling((decimal)totalCount / pageSize);
    }

    /// <summary>
    /// 数据总数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 总页数
    /// </summary>
    public int PageCount { get; set; } = 1;
}

/// <summary>
/// 通用分页数据实体基类
/// </summary>
public class BasePageDataDto<TEntity> where TEntity : class
{
    /// <summary>
    /// 分页数据
    /// </summary>
    public BasePageInfoDto? Page { get; set; }

    /// <summary>
    /// 数据集合
    /// </summary>
    public List<TEntity>? Datas { get; set; }
}