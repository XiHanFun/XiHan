#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageQueryDto
// Guid:c5a5c87a-3140-4f8a-8ce6-4f953c4c1f61
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-09-16 上午 01:38:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Common.Shared.Dtos.Pages;

/// <summary>
/// 通用分页查询基类
/// </summary>
public class PageQueryDto
{
    /// <summary>
    /// 是否查询所有
    /// </summary>
    public bool IsQueryAll { get; set; }

    /// <summary>
    /// 分页信息
    /// </summary>
    public PageInfoDto PageInfo { get; set; } = new();

    /// <summary>
    /// 选择条件集合
    /// </summary>
    public List<SelectConditionDto> SelectConditions { get; set; } = [];

    /// <summary>
    /// 排序条件集合
    /// </summary>
    public List<OrderConditionDto> OrderConditions { get; set; } = [];
}