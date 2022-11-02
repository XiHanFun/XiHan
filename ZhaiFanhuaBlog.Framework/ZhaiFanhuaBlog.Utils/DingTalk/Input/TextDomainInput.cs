namespace ZhaiFanhuaBlog.Utils.DingTalk.Input;

/// <summary>
/// 文本消息
/// </summary>
public class TextDomainInput : IBaseMessage
{
    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; } = string.Empty;
}