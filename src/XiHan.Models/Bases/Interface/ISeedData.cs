#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISeedData
// Guid:e6e582b4-d6c8-46fc-a88a-96c54e730d35
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-13 上午 10:36:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Models.Bases.Interface;

/// <summary>
/// 种子数据接口
/// </summary>
public interface ISeedData<TEntity> where TEntity : class, new()
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    IEnumerable<TEntity> HasData();
}