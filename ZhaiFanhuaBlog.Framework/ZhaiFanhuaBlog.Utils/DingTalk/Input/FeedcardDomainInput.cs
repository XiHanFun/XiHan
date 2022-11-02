namespace ZhaiFanhuaBlog.Utils.DingTalk.Input;

/// <summary>
/// 菜单消息
/// </summary>
public class FeedcardDomainInput : IBaseMessage
{
    /// <summary>
    /// 链接列表
    /// </summary>
    public List<LinkDomainInput>? Links { get; set; }
}