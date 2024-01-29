#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageWhereDto
// Guid:c5a5c87a-3140-4f8a-8ce6-4f953c4c1f61
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-09-16 上午 01:38:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Bases.Responses.Pages;

/// <summary>
/// 通用分页条件基类(包含条件)
/// </summary>
public class PageWhereDto<TWhere> : PageQueryDto where TWhere : class
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public TWhere Where { get; set; } = null!;
}

/// <summary>
/// 通用分页条件过期时间基类(包含条件)
/// </summary>
public class PageWhereExpirableDto<TWhere> : PageWhereDto<TWhere> where TWhere : class
{
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}