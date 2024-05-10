#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HttpGroupEnum
// Guid:5f9c4118-4cb8-43df-8ccc-1e4f0119c452
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 6:36:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Infrastructure.Communication.Https.Enums;

/// <summary>
/// 网络请求组别
/// </summary>
public enum HttpGroupEnum
{
    /// <summary>
    /// 远程
    /// </summary>
    [Description("远程")]
    Remote,

    /// <summary>
    /// 本地
    /// </summary>
    [Description("本地")]
    Local
}