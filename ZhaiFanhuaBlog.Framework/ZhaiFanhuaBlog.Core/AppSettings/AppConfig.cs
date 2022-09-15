// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AppConfig
// Guid:075a4b94-d8d4-4b4e-8e13-83ae6b03e16c
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-22 下午 12:21:06
// ----------------------------------------------------------------

using Microsoft.Extensions.Configuration;

namespace ZhaiFanhuaBlog.Core.AppSettings;

/// <summary>
/// AppConfig
/// </summary>
public static class AppConfig
{
    /// <summary>
    /// 公用配置
    /// </summary>
    public static IConfiguration? Configuration { get; set; }
}