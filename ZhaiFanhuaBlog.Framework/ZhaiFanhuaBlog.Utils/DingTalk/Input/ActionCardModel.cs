// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ActionCardModel
// Guid:348a3a3b-c392-4165-bd06-c5979998c2bd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-03 上午 02:05:18
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Utils.DingTalk.Input;

/// <summary>
/// 卡片消息模型
/// </summary>
public class ActionCardModel : BaseModel
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public string MsgType { get; set; } = "actionCard";

    /// <summary>
    /// 卡片消息
    /// </summary>
    public class ActionCard
    {
        /// <summary>
        /// 首屏会话透出的展示内容，必须
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 是 MarkDown 格式的消息内容，必须
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// 单个按钮的方案，必须（设置此项和singleURL后btns无效）
        /// </summary>
        public string SingleTitle { get; set; } = string.Empty;

        /// <summary>
        /// 点击 SingleTitle 按钮触发的URL，必须
        /// </summary>
        public string SingleUrl { get; set; } = string.Empty;

        /// <summary>
        /// 按钮定位,非必须，0按钮竖直排列，1按钮横向排列
        /// </summary>
        public string? BtnOrientation { get; set; } = string.Empty;

        /// <summary>
        /// 动作按钮，必须，按钮的信息
        /// </summary>
        public List<ActioncardBtn>? ActioncardBtns { get; set; }
    }
}

/// <summary>
/// 动作按钮
/// </summary>
public class ActioncardBtn : IBaseModel
{
    /// <summary>
    /// 按钮方案
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 点击按钮触发的URL
    /// </summary>
    public string ActionURL { get; set; } = string.Empty;
}