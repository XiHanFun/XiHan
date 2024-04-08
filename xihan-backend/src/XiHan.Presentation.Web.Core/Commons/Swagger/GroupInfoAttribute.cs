#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:GroupInfoAttribute
// Guid:2429e1a7-7b23-4e5f-be81-49ae54ead27f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 6:13:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Presentation.Web.Core.Commons.Swagger;

/// <summary>
/// 分组信息 Attribute
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = true)]
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