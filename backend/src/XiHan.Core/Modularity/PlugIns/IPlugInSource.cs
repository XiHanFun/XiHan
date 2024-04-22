#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IPlugInSource
// Guid:1ab6fa8c-e2cd-4a4d-8437-2837a831820b
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 06:05:02
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.Modularity.PlugIns;

/// <summary>
/// 插件源接口
/// </summary>
public interface IPlugInSource
{
    /// <summary>
    /// 获取模块类型
    /// </summary>
    /// <returns></returns>
    Type[] GetModules();
}