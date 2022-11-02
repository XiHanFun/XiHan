namespace ZhaiFanhuaBlog.Utils.DingTalk.Input;

/// <summary>
/// 链接消息
/// </summary>
public class LinkDomainInput : IBaseMessage
{
    /// <summary>
    /// 消息地址
    /// </summary>
    public string MessageUrl { get; set; } = string.Empty;

    /// <summary>
    /// 图片地址
    /// </summary>
    public string PicUrl { get; set; } = string.Empty;

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; } = string.Empty;
}