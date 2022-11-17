﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:GroupInfoAttribute
// Guid:691e0456-6ff4-4b69-909b-286f049a7480
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-11-17 下午 02:18:33
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace ZhaiFanhuaBlog.Extensions.Common.Swagger;

/// <summary>
 /// GroupInfoAttribute
 /// </summary>

[AttributeUsage(AttributeTargets.Field)]
public class GroupInfoAttribute : Attribute
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 版本
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = string.Empty;
}