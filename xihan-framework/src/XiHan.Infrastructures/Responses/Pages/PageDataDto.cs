#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PageDataDto
// Guid:b529170a-f47a-49ee-a231-922e114fc5d8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-14 下午 11:28:38
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructures.Responses.Pages;

/// <summary>
/// 通用分页数据实体基类
/// </summary>
public class PageDataDto<TEntity> where TEntity : class
{
    /// <summary>
    /// 分页数据
    /// </summary>
    public PageInfoDto? PageInfo { get; init; }

    /// <summary>
    /// 数据集合
    /// </summary>
    public List<TEntity>? Data { get; init; }
}