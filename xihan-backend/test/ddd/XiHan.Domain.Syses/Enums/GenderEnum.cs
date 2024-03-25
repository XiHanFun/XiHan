#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:GenderEnum
// Guid:a8ab3c83-7992-4067-955f-a64beffc4106
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-30 下午 06:18:19
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Domain.Syses.Enums;

/// <summary>
/// 性别枚举
/// </summary>
[Description("性别枚举")]
public enum GenderEnum
{
    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    Unknown = 0,

    /// <summary>
    /// 男
    /// </summary>
    [Description("男")]
    Male = 1,

    /// <summary>
    /// 女
    /// </summary>
    [Description("女")]
    Female = 2
}