#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IBaseIdEntity
// Guid:29d06d8a-063b-405c-b8f5-a9cdce847576
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:30:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Models.Bases.Interface;

/// <summary>
/// 通用主键接口
/// </summary>
public interface IBaseIdEntity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    TKey BaseId { get; set; }
}