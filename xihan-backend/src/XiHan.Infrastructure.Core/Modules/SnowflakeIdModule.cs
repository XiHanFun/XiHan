#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SnowflakeIdModule
// Guid:829274de-6874-42ec-949e-40ba18443261
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/29 3:24:35
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Yitter.IdGenerator;

namespace XiHan.Infrastructure.Core.Modules;

/// <summary>
/// SnowflakeIdModule
/// </summary>
public static class SnowflakeIdModule
{
    /// <summary>
    /// 雪花 Id 生成服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddSnowflakeIdModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // 雪花 Id 生成服务
        YitIdHelper.SetIdGenerator(new IdGeneratorOptions()
        {
            WorkerId = 1,
            WorkerIdBitLength = 1,
            SeqBitLength = 6
        });

        return services;
    }
}