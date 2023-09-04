#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:TriggerTypeEnum
// Guid:447d5552-c5b1-4e20-9a91-d9040beedadf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-20 下午 03:28:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Models.Syses.Enums;

/// <summary>
/// 触发器类型
/// </summary>
public enum TriggerTypeEnum
{
    /// <summary>
    /// 定时任务
    /// </summary>
    [Description("定时任务")] Interval = 1,

    /// <summary>
    /// 时间点或者周期性任务
    /// </summary>
    [Description("时间点或者周期性任务")] Cron = 2
}