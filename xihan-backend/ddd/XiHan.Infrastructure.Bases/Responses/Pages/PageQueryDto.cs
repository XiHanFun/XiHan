#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageQueryDto
// Guid:bf5c9903-f8f6-4872-a52c-1a4a72181a42
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/1/25 12:01:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Bases.Responses.Pages;

/// <summary>
/// 通用分页查询基类
/// </summary>
public class PageQueryDto : PageDto
{
    /// <summary>
    /// 是否选择所有
    /// </summary>
    public bool SelectAll { get; set; }

    /// <summary>
    /// 排序方式，支持多字段排序
    /// </summary>
    public List<OrderByDto>? OrderBy { get; set; }
}