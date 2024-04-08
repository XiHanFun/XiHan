#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageResponse
// Guid:1a92ef44-a22b-4083-9302-5353c0d58f1f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 7:23:18
// ----------------------------------------------------------------

#endregion <<版权版本注释>>


#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageResponse
// Guid:1a92ef44-a22b-4083-9302-5353c0d58f1f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 7:23:18
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Core.Apps.RequestOrResponse.Entities;

/// <summary>
/// 通用分页响应
/// </summary>
public class PageResponse
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="page"></param>
    /// <param name="totalCount"></param>
    public PageResponse(PageInfo page, int totalCount)
    {
        Page = page;
        TotalCount = totalCount;
        PageCount = totalCount / page.PageSize + (totalCount % page.PageSize > 0 ? 1 : 0);
    }

    /// <summary>
    /// 分页数据
    /// </summary>
    public PageInfo Page { get; protected init; }

    /// <summary>
    /// 数据总数
    /// </summary>
    public int TotalCount { get; protected init; }

    /// <summary>
    /// 总页数
    /// </summary>
    public int PageCount { get; protected init; }
}

/// <summary>
/// 通用分页响应
/// </summary>
public class PageResponse<T> : PageResponse
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="page"></param>
    /// <param name="datas"></param>
    public PageResponse(PageInfo page, List<T> datas) : base(page, datas.Count)
    {
        Datas = datas;
    }

    /// <summary>
    /// 数据集合
    /// </summary>
    public List<T> Datas { get; protected init; }
}