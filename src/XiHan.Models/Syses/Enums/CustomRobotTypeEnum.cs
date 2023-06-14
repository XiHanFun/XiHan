#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:CustomRobotTypeEnum
// Guid:9f8be55b-25f8-4a40-af16-050ad4ef815f
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-20 下午 03:28:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Models.Syses.Enums;

/// <summary>
/// 自定义机器人类型类型
/// </summary>
public enum CustomRobotTypeEnum
{
    /// <summary>
    /// 钉钉
    /// </summary>
    [Description("钉钉")]
    DingTalk = 1,

    /// <summary>
    /// 企业微信
    /// </summary>
    [Description("企业微信")]
    WeCom = 2,

    /// <summary>
    /// 飞书
    /// </summary>
    [Description("飞书")]
    Lark = 3,
}