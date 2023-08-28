#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageInfoDto
// Guid:f1a45de4-a5d7-459b-90d7-4127e1ef317b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-14 下午 11:27:51
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructures.Responses.Pages;

/// <summary>
/// 通用数据分页信息基类
/// </summary>
public class PageInfoDto : PageDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public PageInfoDto()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public PageInfoDto(int currentIndex, int pageSize, int totalCount)
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