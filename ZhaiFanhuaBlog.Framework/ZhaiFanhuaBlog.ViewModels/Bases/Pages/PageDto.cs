#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PageDto
// Guid:d65e1077-aaa2-407d-8074-2f3fdac31928
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-16 上午 01:37:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace ZhaiFanhuaBlog.ViewModels.Bases.Pages;

/// <summary>
/// 通用分页实体
/// </summary>
public class PageDto : BasePageDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public PageDto()
    { }

    /// <summary>
    /// 构造函数
    /// </summary>
    public PageDto(int currentIndex, int pageSize, int totalCount)
    {
        CurrentIndex = currentIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        PageCount = (int)Math.Ceiling((decimal)totalCount / pageSize);
    }

    /// <summary>
    /// 数据总数
    /// </summary>
    public int TotalCount { get; set; } = 0;

    /// <summary>
    /// 总页数
    /// </summary>
    public int PageCount { get; set; } = 1;
}