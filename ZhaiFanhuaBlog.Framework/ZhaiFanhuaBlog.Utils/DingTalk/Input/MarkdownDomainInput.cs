namespace ZhaiFanhuaBlog.Utils.DingTalk.Input;

/// <summary>
/// Markdown消息
/// </summary>
public class MarkdownDomainInput : IBaseMessage
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; } = string.Empty;
}