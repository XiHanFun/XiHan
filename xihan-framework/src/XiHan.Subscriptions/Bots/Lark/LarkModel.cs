#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:LarkModel
// Guid:5d00cc16-5e63-4fd4-9052-54068c536acf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-06 上午 02:36:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace XiHan.Subscriptions.Bots.Lark;

#region 基本类型

/// <summary>
/// 文本类型
/// </summary>
public class LarkText
{
    /// <summary>
    /// 文本内容
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}

/// <summary>
/// 富文本类型
/// </summary>
public class LarkPost
{
    /// <summary>
    /// 消息标题
    /// </summary>
    [JsonPropertyName("post")]
    public string Post { set; get; } = string.Empty;
}

/// <summary>
/// 文档类型
/// </summary>
public class LarkMarkdown
{
    /// <summary>
    /// 首屏会话透出的展示内容
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { set; get; } = string.Empty;

    /// <summary>
    /// Markdown格式的消息
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { set; get; } = string.Empty;
}

/// <summary>
/// 任务卡片类型
/// </summary>
public class LarkActionCard
{
    /// <summary>
    /// 首屏会话透出的展示内容
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { set; get; } = string.Empty;

    /// <summary>
    /// Markdown格式的消息
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { set; get; } = string.Empty;

    /// <summary>
    /// 单个按钮方案(设置此项后btns无效)
    /// </summary>
    [JsonPropertyName("singleTitle")]
    public string SingleTitle { set; get; } = string.Empty;

    /// <summary>
    /// 单个按钮方案触发的URL(设置此项后btns无效)
    /// </summary>
    [JsonPropertyName("singleURL")]
    public string SingleUrl { set; get; } = string.Empty;

    /// <summary>
    /// 按钮排列，0-按钮竖直排列，1-按钮横向排列
    /// </summary>
    [JsonPropertyName("btnOrientation")]
    public string? BtnOrientation { set; get; } = "0";

    /// <summary>
    /// 按钮的信息：title-按钮方案，actionURL-点击按钮触发的URL
    /// </summary>
    [JsonPropertyName("btns")]
    public List<LarkBtnInfo>? Btns { set; get; }
}

/// <summary>
/// 菜单卡片类型
/// </summary>
public class LarkFeedCard
{
    /// <summary>
    /// 链接列表
    /// </summary>
    [JsonPropertyName("links")]
    public List<LarkFeedCardLink>? Links { get; set; }
}

#endregion 基本类型

#region 辅助类

/// <summary>
/// 菜单卡片类型链接
/// </summary>
public class LarkFeedCardLink
{
    /// <summary>
    /// 消息标题
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { set; get; } = string.Empty;

    /// <summary>
    /// 图片URL
    /// </summary>
    [JsonPropertyName("picURL")]
    public string PicUrl { set; get; } = string.Empty;

    /// <summary>
    /// 点击消息跳转的URL
    /// </summary>
    [JsonPropertyName("messageURL")]
    public string MessageUrl { set; get; } = string.Empty;
}

/// <summary>
/// @指定人(被@人的用户 OpenID 如非群内成员则会被自动过滤)
/// </summary>
public class LarkAt
{
    /// <summary>
    /// 被@的用户ID
    /// </summary>
    [JsonPropertyName("user_id")]
    public List<string>? AtUserIds { set; get; }
}

/// <summary>
/// 按钮信息
/// </summary>
public class LarkBtnInfo
{
    /// <summary>
    /// 按钮方案
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 动作触发的URL
    /// </summary>
    [JsonPropertyName("actionURL")]
    public string ActionUrl { get; set; } = string.Empty;
}

#endregion 辅助类

#region 枚举

/// <summary>
/// 消息类型枚举
/// </summary>
public enum LarkMsgTypeEnum
{
    /// <summary>
    /// 文本类型
    /// </summary>
    [Description("text")] Text,

    /// <summary>
    /// 富文本类型
    /// </summary>
    [Description("post")] Post,

    /// <summary>
    /// 群名片类型
    /// </summary>
    [Description("share_chat")] ShareChat,

    /// <summary>
    /// 图片类型
    /// </summary>
    [Description("image")] Image,

    /// <summary>
    /// 消息卡片类型
    /// </summary>
    [Description("interactive")] InterActive
}

#endregion