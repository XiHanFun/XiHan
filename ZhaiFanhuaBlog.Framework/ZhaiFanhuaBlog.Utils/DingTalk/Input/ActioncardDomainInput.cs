namespace ZhaiFanhuaBlog.Utils.DingTalk.Input;

/// <summary>
/// 卡片消息
/// </summary>
public class ActioncardDomainInput : IBaseMessage
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 唯一标题
    /// </summary>
    public string SingleTitle { get; set; } = string.Empty;

    /// <summary>
    /// 唯一地址
    /// </summary>
    public string SingleUrl { get; set; } = string.Empty;

    /// <summary>
    /// 按钮定位
    /// </summary>
    public string BtnOrientation { get; set; } = string.Empty;

    /// <summary>
    /// 动作按钮
    /// </summary>
    public List<ActioncardBtn>? ActioncardBtns { get; set; }
}

/// <summary>
/// 动作按钮
/// </summary>
public class ActioncardBtn : IBaseMessage
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 动作地址
    /// </summary>
    public string ActionUrl { get; set; } = string.Empty;
}