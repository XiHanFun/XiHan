#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IBaseIdEntity
// Guid:c54f6677-db24-4a58-91a8-97bf61cfdd27
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-06-05 上午 12:42:39
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