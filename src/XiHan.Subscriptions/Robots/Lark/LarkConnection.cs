#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:LarkConnection
// Guid:f2365168-e192-48e0-ac99-32925512f628
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-05-20 下午 02:10:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Subscriptions.Robots.Lark;

/// <summary>
/// LarkConnection
/// </summary>
public class LarkConnection
{
    /// <summary>
    /// 网络挂钩地址
    /// </summary>
    public string WebHookUrl { get; set; } = string.Empty;

    /// <summary>
    /// 访问令牌
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// 机密
    /// </summary>
    public string Secret { get; set; } = string.Empty;
}