#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:StatusEnum
// Guid:06559633-83d0-4a10-84a4-edd4303ab107
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-30 下午 06:21:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Models.Syses.Enums;

/// <summary>
/// 通用状态枚举
/// </summary>
[Description("通用状态枚举")]
public enum StatusEnum
{
    /// <summary>
    /// 启用
    /// </summary>
    [Description("启用")] Enable = 1,

    /// <summary>
    /// 停用
    /// </summary>
    [Description("停用")] Disable = 2
}