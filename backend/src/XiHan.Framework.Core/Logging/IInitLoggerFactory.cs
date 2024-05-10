#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IInitLoggerFactory
// Guid:db6291b1-4cf0-411a-bd17-46811087d140
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 04:05:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.Logging;

/// <summary>
/// 初始化日志工厂接口
/// </summary>
public interface IInitLoggerFactory
{
    /// <summary>
    /// 创建初始化日志
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IInitLogger<T> Create<T>();
}