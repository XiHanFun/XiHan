#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:UserClientInfoDto
// Guid:19605306-3d04-49ed-96af-9534c6403364
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/29 2:01:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Common.Shared.Https.Dtos;

/// <summary>
/// 客户端信息
/// </summary>
public class UserClientInfoDto
{
    /// <summary>
    /// 是否是 ajax 请求
    /// </summary>
    public bool IsAjaxRequest { get; set; }

    /// <summary>
    /// 语言
    /// </summary>
    public string Language { get; set; } = string.Empty;

    /// <summary>
    /// 来源页面
    /// </summary>
    public string Referer { get; set; } = string.Empty;

    /// <summary>
    /// 代理信息
    /// </summary>
    public string Agent { get; set; } = string.Empty;

    /// <summary>
    /// 设备类型
    /// </summary>
    public string DeviceType { get; set; } = string.Empty;

    /// <summary>
    /// 系统名称
    /// </summary>
    public string OsName { get; set; } = string.Empty;

    /// <summary>
    /// 系统版本
    /// </summary>
    public string OsVersion { get; set; } = string.Empty;

    /// <summary>
    /// 浏览器名称
    /// </summary>
    public string BrowserName { get; set; } = string.Empty;

    /// <summary>
    /// 浏览器版本
    /// </summary>
    public string BrowserVersion { get; set; } = string.Empty;
}