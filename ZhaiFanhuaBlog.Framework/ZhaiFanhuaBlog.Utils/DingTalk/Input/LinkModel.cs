// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:LinkModel
// Guid:ce319d9f-2ed3-4bc5-84ca-4a3585d46aeb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-03 上午 02:06:19
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Utils.DingTalk.Input;

/// <summary>
/// 链接消息模型
/// </summary>
public class LinkModel : BaseModel
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public string MsgType { get; set; } = "link";

    /// <summary>
    /// 链接消息
    /// </summary>
    public class Link
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
        public string Text { get; set; } = string.Empty;
    }
}