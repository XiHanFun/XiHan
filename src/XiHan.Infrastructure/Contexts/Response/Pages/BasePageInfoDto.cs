#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BasePageInfoDto
// Guid:ac953e11-5c32-44d5-a450-d377ef3a0453
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-04 下午 11:15:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Contexts.Response.Pages;

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