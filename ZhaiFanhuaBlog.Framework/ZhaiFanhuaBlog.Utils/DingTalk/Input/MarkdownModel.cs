// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:MarkdownModel
// Guid:282b5af9-8f3b-4ff3-b42b-c26570152f74
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-03 上午 02:07:00
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Utils.DingTalk.Input;

/// <summary>
/// Markdown消息模型
/// </summary>
public class MarkdownModel : BaseModel
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public string MsgType { get; set; } = "markdown";

    /// <summary>
    /// Markdown消息
    /// </summary>
    public class Markdown
    {
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