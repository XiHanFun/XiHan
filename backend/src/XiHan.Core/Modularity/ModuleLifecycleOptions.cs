#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ModuleLifecycleOptions
// Guid:7b771171-55c9-48ae-ba9b-9028cad9c69b
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 02:46:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.Collections;
using XiHan.Core.Modularity.Abstracts;

namespace XiHan.Core.Modularity;

/// <summary>
/// 模块生命周期选项
/// </summary>
public class ModuleLifecycleOptions
{
    /// <summary>
    /// 贡献者
    /// </summary>
    public ITypeList<IModuleLifecycleContributor> Contributors { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public ModuleLifecycleOptions()
    {
        Contributors = new TypeList<IModuleLifecycleContributor>();
    }
}