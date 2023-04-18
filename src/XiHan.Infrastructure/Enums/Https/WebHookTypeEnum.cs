#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:WebHookTypeEnum
// Guid:4f48b1ce-89af-4408-9da3-c59deb11bce7
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-19 上午 02:55:08
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Infrastructure.Enums.Https;

/// <summary>
/// 网络挂钩类型
/// </summary>
public enum WebHookTypeEnum
{
    /// <summary>
    /// 钉钉
    /// </summary>
    [Description("钉钉")]
    DingTalk = 1,

    /// <summary>
    /// 企业微信
    /// </summary>
    [Description("企业微信")]
    WeCom,
}