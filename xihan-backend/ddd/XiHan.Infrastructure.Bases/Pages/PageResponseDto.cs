#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageResponseDto
// Guid:f1a45de4-a5d7-459b-90d7-4127e1ef317b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-14 下午 11:27:51
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructure.Bases.Pages.Dtos;

namespace XiHan.Infrastructure.Bases.Pages;

/// <summary>
/// 通用分页响应基类
/// </summary>
public class PageResponseDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public PageResponseDto(PageInfoDto page, int totalCount)
    {
        Page = page;
        TotalCount = totalCount;
        PageCount = (int)Math.Ceiling((decimal)totalCount / page.PageSize);
    }

    /// <summary>
    /// 分页数据
    /// </summary>
    public PageInfoDto Page { get; set; }

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
public class PageResponseDataDto<TEntity> where TEntity : class
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="page"></param>
    /// <param name="totalCount"></param>
    /// <param name="datas"></param>
    public PageResponseDataDto(PageInfoDto page, int totalCount, IEnumerable<TEntity> datas)
    {
        PageResponse = new PageResponseDto(page, totalCount);
        Datas = datas.ToList();
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="pageResponse"></param>
    /// <param name="datas"></param>
    public PageResponseDataDto(PageResponseDto pageResponse, IEnumerable<TEntity> datas)
    {
        PageResponse = pageResponse;
        Datas = datas.ToList();
    }

    /// <summary>
    /// 分页数据
    /// </summary>
    public PageResponseDto PageResponse { get; init; }

    /// <summary>
    /// 数据集合
    /// </summary>
    public List<TEntity> Datas { get; init; }
}