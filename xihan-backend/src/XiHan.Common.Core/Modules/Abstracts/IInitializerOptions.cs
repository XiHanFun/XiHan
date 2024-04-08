#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:InitializerOptions
// Guid:a1143369-3d69-4dd1-adf1-ccbd124645ea
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/30 8:19:24
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Common.Core.Modules.Abstracts;

/// <summary>
/// 模块初始化参数接口
/// </summary>
public interface IInitializerOptions
{
    /// <summary>
    /// 是否启用模块初始化
    /// </summary>
    bool Enabled { get; set; }
}