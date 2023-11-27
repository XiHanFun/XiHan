#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:LarkConnection
// Guid:f2365168-e192-48e0-ac99-32925512f628
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-05-20 下午 02:10:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Subscriptions.Bots.Lark;

/// <summary>
/// LarkConnection
/// </summary>
public class LarkConnection
{
    private const string _defaultLarkWebHookUrl = "https://open.feishu.cn/open-apis/bot/v2/hook";

    private const string _defaultLarkUploadUrl = "https://open.feishu.cn/open-apis/im/v1/images";

    private string? _webHookUrl;

    private string? _uploadUrl;

    /// <summary>
    /// 网络挂钩地址
    /// </summary>
    public string WebHookUrl
    {
        get => _webHookUrl ??= _defaultLarkWebHookUrl;
        set => _webHookUrl = value;
    }

    /// <summary>
    /// 文件上传地址
    /// </summary>
    public string UploadUrl
    {
        get => _uploadUrl ??= _defaultLarkUploadUrl;
        set => _uploadUrl = value;
    }

    /// <summary>
    /// 访问令牌
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// 机密
    /// </summary>
    public string? Secret { get; set; }

    /// <summary>
    /// 关键字
    /// </summary>
    public string? KeyWord { get; set; }
}