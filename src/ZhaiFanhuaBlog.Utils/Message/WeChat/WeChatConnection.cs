#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WeChatConnection
// Guid:e090aec3-2ede-4510-8ec2-6e542f34c26d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-24 上午 02:35:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace ZhaiFanhuaBlog.Utils.Message.WeChat;

/// <summary>
/// WeChatConnection
/// </summary>
public class WeChatConnection
{
    /// <summary>
    /// 网络挂钩地址
    /// </summary>
    public string WebHookUrl { get; set; } = string.Empty;

    /// <summary>
    /// 文件上传地址
    /// </summary>
    public string UploadkUrl { get; set; } = string.Empty;

    /// <summary>
    /// 访问令牌
    /// </summary>
    public string Key { get; set; } = string.Empty;
}