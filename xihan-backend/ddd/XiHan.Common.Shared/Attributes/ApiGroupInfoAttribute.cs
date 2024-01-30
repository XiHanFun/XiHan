#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApiGroupInfoAttribute
// Guid:61ffb243-f15b-4293-ab11-52d7e16cbd78
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/1/30 22:06:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Common.Shared.Attributes;

/// <summary>
/// 接口分组信息特性
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class ApiGroupInfoAttribute : Attribute
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