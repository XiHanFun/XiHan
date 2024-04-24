#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IHostEnvironment
// Guid:0d40d970-dae1-43be-828a-51f964770ec0
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 02:08:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.Application.Abstracts;

/// <summary>
/// 宿主环境接口
/// </summary>
public interface IHostEnvironment
{
    /// <summary>
    /// 环境名称
    /// </summary>
    string? EnvironmentName { get; set; }
}