#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PreConfigureActionList
// Guid:36fc2afd-e4d2-4a21-bc41-5f442e559a6c
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 03:53:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.Options;

/// <summary>
/// 预配置泛型委托列表
/// </summary>
public class PreConfigureActionList<TOptions> : List<Action<TOptions>>
{
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="options"></param>
    public void Configure(TOptions options)
    {
        foreach (var action in this)
        {
            action(options);
        }
    }

    /// <summary>
    /// 配置
    /// </summary>
    /// <returns></returns>
    public TOptions Configure()
    {
        var options = Activator.CreateInstance<TOptions>();
        Configure(options);
        return options;
    }
}