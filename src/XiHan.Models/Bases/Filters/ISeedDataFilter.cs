#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISeedDataFilter
// Guid:ef84c33b-4b80-4c6a-9cb2-6ba854573f3f
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:31:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Models.Bases.Filters;

/// <summary>
/// 种子数据接口过滤器
/// </summary>
public interface ISeedDataFilter<TEntity> where TEntity : class, new()
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    IEnumerable<TEntity> HasData();
}