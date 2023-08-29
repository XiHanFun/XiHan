#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:WeComUploadResultDto
// Guid:9385c398-2a75-4259-9328-c0039cd26823
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/10 2:20:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Subscriptions.WebHooks.WeCom;

/// <summary>
/// 文件上传返回结果
/// </summary>
public class WeComUploadResultDto
{
    /// <summary>
    /// 返回消息
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 介质ID
    /// </summary>
    public string MediaId { get; set; } = string.Empty;
}