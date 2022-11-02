// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TextModel
// Guid:161ba195-0060-4100-a81b-083ea1d5453e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-03 上午 02:07:32
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Utils.DingTalk.Input;

/// <summary>
/// 文本消息模型
/// </summary>
public class TextModel : BaseModel
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="text"></param>
    public TextModel(Text text)
    {
        Msgtype = "text";
        Text = text;
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public string Msgtype { get; set; }

    /// <summary>
    /// 文本消息
    /// </summary>
    public Text Text { get; set; }
}

/// <summary>
/// 文本消息
/// </summary>
public class Text
{
    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; } = string.Empty;
}