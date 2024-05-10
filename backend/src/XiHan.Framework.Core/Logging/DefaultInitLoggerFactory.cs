#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DefaultInitLoggerFactory
// Guid:f5848e6a-0670-47cb-a1b9-52c96a2c8327
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 23:07:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Framework.Core.System.Collections.Generic.Extensions;

namespace XiHan.Framework.Core.Logging;

/// <summary>
/// 默认初始化日志工厂
/// </summary>
public class DefaultInitLoggerFactory : IInitLoggerFactory
{
    private readonly Dictionary<Type, object> _cache = [];

    /// <summary>
    /// 创建初始化日志
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public virtual IInitLogger<T> Create<T>()
    {
        return (IInitLogger<T>)_cache.GetOrAdd(typeof(T), () => new DefaultInitLogger<T>()); ;
    }
}