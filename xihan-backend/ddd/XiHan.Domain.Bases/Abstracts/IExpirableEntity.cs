#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IExpirableEntity
// Guid:178047f6-67c7-4bb9-871c-faa20bf4cd46
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/1/17 8:39:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Domain.Bases.Abstracts;

/// <summary>
/// 通用过期时间接口
/// </summary>
public interface IExpirableEntity
{
    /// <summary>
    /// 开始时间
    /// </summary>
    DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    DateTime? EndTime { get; set; }
}