#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:XiHanHostEnvironment
// Guid:a1dcf541-371a-45e6-9777-3637aea78815
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 02:07:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.Application;

/// <summary>
/// 曦寒宿主环境
/// </summary>
public class XiHanHostEnvironment : IXiHanHostEnvironment
{
    /// <summary>
    /// 环境名称
    /// </summary>
    public string? EnvironmentName { get; set; }
}