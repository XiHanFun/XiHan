#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:WeComConnection
// Guid:e090aec3-2ede-4510-8ec2-6e542f34c26d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-24 上午 02:35:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>


namespace XiHan.Subscriptions.Bots.WeCom;

/// <summary>
/// WeChatConnection
/// </summary>
public class WeComConnection
{
    private const string DefaultWeComWebHookUrl = "https://qyapi.weixin.qq.com/cgi-bin/webhook/send";

    private const string DefaultWeComUploadUrl = "https://qyapi.weixin.qq.com/cgi-bin/webhook/upload_media";

    private string? _webHookUrl;

    private string? _uploadUrl;

    /// <summary>
    /// 网络挂钩地址
    /// </summary>
    public string WebHookUrl
    {
        get => _webHookUrl ??= DefaultWeComWebHookUrl;
        set => _webHookUrl = value;
    }

    /// <summary>
    /// 文件上传地址
    /// </summary>
    public string UploadUrl
    {
        get => _uploadUrl ??= DefaultWeComUploadUrl;
        set => _uploadUrl = value;
    }

    /// <summary>
    /// 访问令牌
    /// </summary>
    public string Key { get; set; } = string.Empty;
}