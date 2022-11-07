﻿// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WeChatModel
// Guid:479a9594-c7fb-42e7-bc6a-2c51c7e58b28
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 12:47:53
// ----------------------------------------------------------------

using Newtonsoft.Json;

namespace ZhaiFanhuaBlog.Utils.MessagePush.WeChat;

#region 基本类型

/// <summary>
/// Text类型
/// </summary>
public class Text : At
{
    /// <summary>
    /// 文本内容，最长不超过2048个字节，必须是utf8编码
    /// </summary>
    [JsonProperty(PropertyName = "content")]
    public string Content { get; set; } = string.Empty;
}

/// <summary>
/// Markdown类型
/// </summary>
public class Markdown
{
    /// <summary>
    /// Markdown格式的消息，最长不超过4096个字节，必须是utf8编码
    /// </summary>
    [JsonProperty(PropertyName = "content")]
    public string Content { get; set; } = string.Empty;
}

/// <summary>
/// Image类型，base64编码前最大不能超过2M，支持JPG、PNG格式
/// </summary>
public class Image
{
    /// <summary>
    /// 图片内容（base64编码前）的md5值
    /// </summary>
    [JsonProperty(PropertyName = "md5")]
    public string Md5 { get; set; } = string.Empty;

    /// <summary>
    /// 图片内容的base64编码
    /// </summary>
    [JsonProperty(PropertyName = "base64")]
    public string Base64 { get; set; } = string.Empty;
}

/// <summary>
/// 图文类型
/// </summary>
public class News
{
    /// <summary>
    /// 图文消息，一个图文消息支持1到8条图文
    /// </summary>
    [JsonProperty(PropertyName = "articles")]
    public List<Articles>? Articles { get; set; }
}

/// <summary>
/// 文件类型
/// </summary>
public class File
{
    /// <summary>
    /// 文件id，通过下文的文件上传接口获取
    /// </summary>
    [JsonProperty(PropertyName = "media_id")]
    public string MediaId { get; set; } = string.Empty;
}

/// <summary>
/// 模版卡片类型
/// </summary>
public class TemplateCard
{
    /// <summary>
    /// 模版卡片的模版类型，文本通知模版卡片的类型为text_notice，图文展示模版卡片的类型为news_notice
    /// </summary>
    [JsonProperty(PropertyName = "card_type")]
    public string CardType { get; set; } = string.Empty;

    /// <summary>
    /// 卡片来源样式信息，非必填，不需要来源样式可不填写
    /// </summary>
    [JsonProperty(PropertyName = "source")]
    public Source? Source { get; set; }

    /// <summary>
    /// 模版卡片的主要内容，包括一级标题和标题辅助信息
    /// </summary>
    [JsonProperty(PropertyName = "main_title")]
    public MainTitle? MainTitle { get; set; }

    /// <summary>
    /// 关键数据样式，非必填
    /// </summary>
    [JsonProperty(PropertyName = "emphasis_content")]
    public EmphasisContent? EmphasisContent { get; set; }

    /// <summary>
    /// 引用文献样式，建议不与关键数据共用
    /// </summary>
    [JsonProperty(PropertyName = "quote_area")]
    public QuoteArea? QuoteArea { get; set; }

    /// <summary>
    /// 二级普通文本，非必填，建议不超过112个字
    /// 模版卡片主要内容的一级标题main_title.title和二级普通文本sub_title_text必须有一项填写
    /// </summary>
    [JsonProperty(PropertyName = "sub_title_text")]
    public string? SubTitleText { get; set; } = string.Empty;

    /// <summary>
    /// 二级标题+文本列表，该字段可为空数组，但有数据的话需确认对应字段是否必填，列表长度不超过6
    /// </summary>
    [JsonProperty(PropertyName = "horizontal_content_list")]
    public List<HorizontalContent>? HorizontalContents { get; set; }

    /// <summary>
    /// 跳转指引样式的列表，该字段可为空数组，但有数据的话需确认对应字段是否必填，列表长度不超过3
    /// </summary>
    [JsonProperty(PropertyName = "jump_list")]
    public List<Jump>? Jumps { get; set; }

    /// <summary>
    /// 整体卡片的点击跳转事件，text_notice模版卡片中该字段为必填项
    /// </summary>
    [JsonProperty(PropertyName = "card_action")]
    public CardAction CardAction { get; set; } = new CardAction();
}

#endregion 基本类型

#region 辅助类

/// <summary>
/// @指定人(被@人的手机号和被@人的用户 userid 如非群内成员则会被自动过滤)
/// </summary>
public class At
{
    /// <summary>
    /// 被@的用户微信号，@all表示提醒所有人 示例：["wangqing","@all"]
    /// </summary>
    [JsonProperty(PropertyName = "mentioned_list")]
    public List<string>? Mentioneds { set; get; }

    /// <summary>
    /// 被@的手机号，@all表示提醒所有人 示例：["13800001111","@all"]
    /// </summary>
    [JsonProperty(PropertyName = "mentioned_mobile_list")]
    public List<string>? MentionedMobiles { set; get; }
}

/// <summary>
/// 文章
/// </summary>
public class Articles
{
    /// <summary>
    /// 标题，不超过128个字节，超过会自动截断
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 描述，非必填，不超过512个字节，超过会自动截断
    /// </summary>
    [JsonProperty(PropertyName = "description")]
    public string? Description { get; set; } = string.Empty;

    /// <summary>
    /// 点击后跳转的链接
    /// </summary>
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// 图文消息的图片链接，非必填，支持JPG、PNG格式，较好的效果为大图 1068*455，小图150*150
    /// </summary>
    [JsonProperty(PropertyName = "picurl")]
    public string? PicUrl { get; set; } = string.Empty;
}

/// <summary>
/// 卡片来源样式信息，不需要来源样式可不填写
/// </summary>
public class Source
{
    /// <summary>
    /// 来源图片的url，非必填
    /// </summary>
    [JsonProperty(PropertyName = "icon_url")]
    public string? IconUrl { get; set; } = string.Empty;

    /// <summary>
    /// 来源图片的描述，非必填，建议不超过13个字
    /// </summary>
    [JsonProperty(PropertyName = "desc")]
    public string? Desc { get; set; } = string.Empty;

    /// <summary>
    /// 来源文字的颜色，非必填，目前支持：0(默认) 灰色，1 黑色，2 红色，3 绿色
    /// </summary>
    [JsonProperty(PropertyName = "desc_color")]
    public int? DescColor { get; set; } = 0;
}

/// <summary>
/// 模版卡片的主要内容
/// </summary>
public class MainTitle
{
    /// <summary>
    /// 一级标题，非必填，建议不超过26个字
    /// 模版卡片主要内容的一级标题main_title.title和二级普通文本sub_title_text必须有一项填写
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string? Title { get; set; } = string.Empty;

    /// <summary>
    /// 标题辅助信息，非必填，建议不超过30个字
    /// </summary>
    [JsonProperty(PropertyName = "desc")]
    public string? Desc { get; set; } = string.Empty;
}

/// <summary>
/// 关键数据样式
/// </summary>
public class EmphasisContent
{
    /// <summary>
    /// 关键数据样式的数据内容，非必填，建议不超过10个字
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string? Title { get; set; } = string.Empty;

    /// <summary>
    /// 关键数据样式的数据描述内容，非必填，建议不超过15个字
    /// </summary>
    [JsonProperty(PropertyName = "desc")]
    public string? Desc { get; set; } = string.Empty;
}

/// <summary>
/// 引用文献样式，建议不与关键数据共用
/// </summary>
public class QuoteArea
{
    /// <summary>
    /// 引用文献样式区域点击事件，0或不填代表没有点击事件，1 代表跳转url，2 代表跳转小程序
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public int? Type { get; set; } = 0;

    /// <summary>
    /// 点击跳转的url，quote_area.type是1时必填
    /// </summary>
    [JsonProperty(PropertyName = "url")]
    public string? Url { get; set; } = string.Empty;

    /// <summary>
    /// 点击跳转的小程序的appid，quote_area.type是2时必填
    /// </summary>
    [JsonProperty(PropertyName = "appid")]
    public string? AppId { get; set; } = string.Empty;

    /// <summary>
    /// 点击跳转的小程序的pagepath，quote_area.type是2时选填
    /// </summary>
    [JsonProperty(PropertyName = "pagepath")]
    public string? PagePath { get; set; } = string.Empty;

    /// <summary>
    /// 引用文献样式的标题
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string? Title { get; set; } = string.Empty;

    /// <summary>
    /// 引用文献样式的引用文案
    /// </summary>
    [JsonProperty(PropertyName = "quote_text")]
    public string? QuoteText { get; set; } = string.Empty;
}

/// <summary>
/// 二级标题+文本
/// </summary>
public class HorizontalContent
{
    /// <summary>
    /// 链接类型，0或不填代表是普通文本，1 代表跳转url，2 代表下载附件，3 代表@员工
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public int? Type { get; set; } = 0;

    /// <summary>
    /// 二级标题，建议不超过5个字
    /// </summary>
    [JsonProperty(PropertyName = "keyname")]
    public string KeyName { get; set; } = string.Empty;

    /// <summary>
    /// 二级文本，如果horizontal_content_list.type是2，该字段代表文件名称（要包含文件类型），建议不超过26个字
    /// </summary>
    [JsonProperty(PropertyName = "value")]
    public string? Value { get; set; } = string.Empty;

    /// <summary>
    /// 链接跳转的url，horizontal_content_list.type是1时必填
    /// </summary>
    [JsonProperty(PropertyName = "url")]
    public string? Url { get; set; } = string.Empty;

    /// <summary>
    /// 附件的media_id，horizontal_content_list.type是2时必填
    /// </summary>
    [JsonProperty(PropertyName = "media_id")]
    public string? MediaId { get; set; } = string.Empty;

    /// <summary>
    /// 被@的成员的userid，horizontal_content_list.type是3时必填
    /// </summary>
    [JsonProperty(PropertyName = "userid")]
    public string? UserId { get; set; } = string.Empty;
}

/// <summary>
/// 跳转指引样式
/// </summary>
public class Jump
{
    /// <summary>
    /// 跳转链接类型，0或不填代表不是链接，1 代表跳转url，2 代表跳转小程序
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public int? Type { get; set; } = 0;

    /// <summary>
    /// 跳转链接样式的文案内容，建议不超过13个字
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 跳转链接的url，jump_list.type是1时必填
    /// </summary>
    [JsonProperty(PropertyName = "url")]
    public string? Url { get; set; } = string.Empty;

    /// <summary>
    /// 跳转链接的小程序的appid，jump_list.type是2时必填
    /// </summary>
    [JsonProperty(PropertyName = "appid")]
    public string? AppId { get; set; } = string.Empty;

    /// <summary>
    /// 跳转链接的小程序的pagepath，jump_list.type是2时选填
    /// </summary>
    [JsonProperty(PropertyName = "pagepath")]
    public string? PagePath { get; set; } = string.Empty;
}

/// <summary>
/// 整体卡片的点击跳转事件，text_notice模版卡片中该字段为必填项
/// </summary>
public class CardAction
{
    /// <summary>
    /// 卡片跳转类型，1 代表跳转url，2 代表打开小程序。text_notice模版卡片中该字段取值范围为[1,2]
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public int Type { get; set; } = 1;

    /// <summary>
    /// 跳转事件的url，card_action.type是1时必填
    /// </summary>
    [JsonProperty(PropertyName = "url")]
    public string? Url { get; set; } = string.Empty;

    /// <summary>
    /// 跳转事件的小程序的appid，card_action.type是2时必填
    /// </summary>
    [JsonProperty(PropertyName = "appid")]
    public string? AppId { get; set; } = string.Empty;

    /// <summary>
    /// 跳转事件的小程序的pagepath，card_action.type是2时选填
    /// </summary>
    [JsonProperty(PropertyName = "pagepath")]
    public string? PagePath { get; set; } = string.Empty;
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
    /// 文档类型
    /// </summary>
    markdown,

    /// <summary>
    /// 图片类型
    /// </summary>
    image,

    /// <summary>
    /// 图文类型
    /// </summary>
    news,

    /// <summary>
    /// 文件类型
    /// </summary>
    file,

    /// <summary>
    /// 模版卡片类型
    /// </summary>
    template_card
}

/// <summary>
/// 消息类型枚举
/// </summary>
public enum TemplateCardType
{
    /// <summary>
    /// 文本通知类型，属于模版卡片类型
    /// </summary>
    text_notice,

    /// <summary>
    /// 图文展示类型，属于模版卡片类型
    /// </summary>
    news_notice
}