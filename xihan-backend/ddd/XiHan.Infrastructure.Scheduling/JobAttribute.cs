#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:JobAttribute
// Guid:2951a794-45b7-487e-a1c4-ef46f1d23623
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/19 2:47:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Scheduling;

/// <summary>
/// JobAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class JobAttribute : Attribute
{
    /// <summary>
    /// 任务名称
    /// </summary>
    public string JobName { get; set; } = string.Empty;

    /// <summary>
    /// 任务分组
    /// </summary>
    public string JobGroup { get; set; } = string.Empty;

    /// <summary>
    /// 任务描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; } = true;
}