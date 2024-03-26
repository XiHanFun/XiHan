#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IBaseExpirable
// Guid:7675b01f-1006-45e3-9616-fa10f57681e6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 7:03:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Domain.Core.Models.Entities.Abstracts;

/// <summary>
/// 通用过期时间接口
/// </summary>
public interface IBaseExpirable
{
    /// <summary>
    /// 开始时间
    /// </summary>
    DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    DateTime? EndTime { get; set; }
}