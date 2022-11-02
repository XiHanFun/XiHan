// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:FeedCardModel
// Guid:790d691a-e5c6-4e3d-9f42-d40a6a1d438e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-03 上午 02:04:23
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Utils.DingTalk.Input;

/// <summary>
/// 菜单消息模型
/// </summary>
public class FeedCardModel : BaseModel
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public string MsgType { get; set; } = "feedCard";

    /// <summary>
    /// 菜单消息
    /// </summary>
    public class FeedCard
    {
        /// <summary>
        /// 链接列表
        /// </summary>
        public List<LinkModel>? Links { get; set; }
    }
}