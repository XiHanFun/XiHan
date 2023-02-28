#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BasePageDto
// Guid:1e6dfc38-1188-40e6-a6f0-5dee44d6209b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-02 上午 01:03:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Contexts.Response.Pages;

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