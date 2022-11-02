namespace ZhaiFanhuaBlog.Utils.DingTalk;

/// <summary>
/// 消息类型
/// </summary>
public enum MessageTypeEnum
{
    /// <summary>
    /// 文本消息
    /// </summary>
    TextMessage,

    /// <summary>
    /// 链接消息
    /// </summary>
    LinkMessage,

    /// <summary>
    /// Markdown消息
    /// </summary>
    MarkdownMessage,

    /// <summary>
    /// 卡片消息
    /// </summary>
    ActionCardMessage,

    /// <summary>
    /// 菜单消息
    /// </summary>
    FeedCardMessage
}