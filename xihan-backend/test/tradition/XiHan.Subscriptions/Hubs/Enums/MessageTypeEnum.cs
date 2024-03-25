#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:MessageTypeEnum
// Guid:330e94ac-9b76-4a42-81e6-ce6faa58a002
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/26 2:01:10
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Subscriptions.Hubs.Enums;

/// <summary>
/// 消息类型枚举
/// </summary>
[Description("消息类型枚举")]
public enum MessageTypeEnum
{
    /// <summary>
    /// 普通信息
    /// </summary>
    [Description("消息")] Info = 0,

    /// <summary>
    /// 成功提示
    /// </summary>
    [Description("成功")] Success = 1,

    /// <summary>
    /// 警告提示
    /// </summary>
    [Description("警告")] Warning = 2,

    /// <summary>
    /// 错误提示
    /// </summary>
    [Description("错误")] Error = 3
}