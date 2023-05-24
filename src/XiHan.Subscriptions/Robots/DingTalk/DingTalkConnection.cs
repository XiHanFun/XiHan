#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DingTalkConnection
// Guid:a1d2038d-d4a1-4a9d-a3af-a437b6d13f6c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-24 上午 02:27:30
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Subscriptions.Robots.DingTalk;

/// <summary>
/// DingTalkConnection
/// </summary>
public class DingTalkConnection
{
    private const string DefaultDingTalkWebHookUrl = "https://oapi.dingtalk.com/robot/send";

    private string? _webHookUrl;

    /// <summary>
    /// 网络挂钩地址
    /// </summary>
    public string WebHookUrl
    {
        get => _webHookUrl ??= DefaultDingTalkWebHookUrl;
        set => _webHookUrl = value;
    }

    /// <summary>
    /// 访问令牌
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// 机密
    /// </summary>
    public string Secret { get; set; } = string.Empty;
}