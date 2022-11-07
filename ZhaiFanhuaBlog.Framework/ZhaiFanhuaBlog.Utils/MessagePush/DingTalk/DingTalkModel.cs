﻿// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DingTalkModel
// Guid:5d00cc16-5e63-4fd4-9052-54068c536acf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-06 上午 02:36:46
// ----------------------------------------------------------------

using Newtonsoft.Json;

namespace ZhaiFanhuaBlog.Utils.MessagePush.DingTalk;

#region 基本类型

/// <summary>
/// 文本类型
/// </summary>
public class Text
{
    /// <summary>
    /// 文本内容
    /// </summary>
    [JsonProperty(PropertyName = "content")]
    public string Content { get; set; } = string.Empty;
}

/// <summary>
/// 链接类型
/// </summary>
public class Link
{
    /// <summary>
    /// 消息标题
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { set; get; } = string.Empty;

    /// <summary>
    /// 消息内容
    /// </summary>
    [JsonProperty(PropertyName = "text")]
    public string Text { set; get; } = string.Empty;

    /// <summary>
    /// 图片URL
    /// </summary>
    [JsonProperty(PropertyName = "picUrl")]
    public string PicUrl { set; get; } = string.Empty;

    /// <summary>
    /// 点击消息跳转的URL
    /// </summary>
    [JsonProperty(PropertyName = "messageUrl")]
    public string MessageUrl { set; get; } = string.Empty;
}

/// <summary>
/// 文档类型
/// </summary>
public class Markdown
{
    /// <summary>
    /// 首屏会话透出的展示内容
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { set; get; } = string.Empty;

    /// <summary>
    /// Markdown格式的消息
    /// </summary>
    [JsonProperty(PropertyName = "text")]
    public string Text { set; get; } = string.Empty;
}

/// <summary>
/// 任务卡片类型
/// </summary>
public class ActionCard
{
    /// <summary>
    /// 首屏会话透出的展示内容
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { set; get; } = string.Empty;

    /// <summary>
    /// Markdown格式的消息
    /// </summary>
    [JsonProperty(PropertyName = "text")]
    public string Text { set; get; } = string.Empty;

    /// <summary>
    /// 单个按钮方案(设置此项后btns无效)
    /// </summary>
    [JsonProperty(PropertyName = "singleTitle")]
    public string SingleTitle { set; get; } = string.Empty;

    /// <summary>
    /// 单个按钮方案触发的URL(设置此项后btns无效)
    /// </summary>
    [JsonProperty(PropertyName = "singleURL")]
    public string SingleUrl { set; get; } = string.Empty;

    /// <summary>
    /// 按钮排列，0-按钮竖直排列，1-按钮横向排列
    /// </summary>
    [JsonProperty(PropertyName = "btnOrientation")]
    public string? BtnOrientation { set; get; } = string.Empty;

    /// <summary>
    /// 按钮的信息：title-按钮方案，actionURL-点击按钮触发的URL
    /// </summary>
    [JsonProperty(PropertyName = "btns")]
    public List<BtnInfo>? Btns { set; get; }
}

/// <summary>
/// 菜单卡片类型
/// </summary>
public class FeedCard
{
    /// <summary>
    /// 链接列表
    /// </summary>
    [JsonProperty(PropertyName = "links")]
    public List<FeedCardLink>? Links { get; set; }
}

#endregion 基本类型

#region 辅助类

/// <summary>
/// 菜单卡片类型链接
/// </summary>
public class FeedCardLink
{
    /// <summary>
    /// 消息标题
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { set; get; } = string.Empty;

    /// <summary>
    /// 图片URL
    /// </summary>
    [JsonProperty(PropertyName = "picURL")]
    public string PicUrl { set; get; } = string.Empty;

    /// <summary>
    /// 点击消息跳转的URL
    /// </summary>
    [JsonProperty(PropertyName = "messageURL")]
    public string MessageUrl { set; get; } = string.Empty;
}

/// <summary>
/// @指定人(被@人的手机号和被@人的用户 userid 如非群内成员则会被自动过滤)
/// </summary>
public class At
{
    /// <summary>
    /// 被@的手机号
    /// </summary>
    [JsonProperty(PropertyName = "atMobiles")]
    public List<string>? AtMobiles { set; get; }

    /// <summary>
    /// 被@的用户ID
    /// </summary>
    [JsonProperty(PropertyName = "atUserIds")]
    public List<string>? AtUserIds { set; get; }

    /// <summary>
    /// 是否@所有人(如要 @所有人为 true，反之用 false)
    /// </summary>
    [JsonProperty(PropertyName = "isAtAll")]
    public bool IsAtAll { set; get; }
}

/// <summary>
/// 按钮信息
/// </summary>
public class BtnInfo
{
    /// <summary>
    /// 按钮方案
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 动作触发的URL
    /// </summary>
    [JsonProperty(PropertyName = "actionURL")]
    public string ActionUrl { get; set; } = string.Empty;
}

/// <summary>
/// 结果信息
/// </summary>
public class ResultInfo
{
    /// <summary>
    /// 结果代码 成功 0
    /// </summary>
    [JsonProperty(PropertyName = "errcode")]
    public string ErrCode { get; set; } = string.Empty;

    /// <summary>
    /// 结果消息 成功 ok
    /// </summary>
    [JsonProperty(PropertyName = "errmsg")]
    public string ErrMsg { get; set; } = string.Empty;
}

#endregion 辅助类

/// <summary>
/// 消息类型枚举
/// </summary>
public enum MsgTypeEnum
{
    /// <summary>
    /// 文本类型
    /// </summary>
    text,

    /// <summary>
    /// 链接类型
    /// </summary>
    link,

    /// <summary>
    /// 文档类型
    /// </summary>
    markdown,

    /// <summary>
    /// 任务卡片类型
    /// </summary>
    actionCard,

    /// <summary>
    /// 菜单卡片类型
    /// </summary>
    feedCard
}