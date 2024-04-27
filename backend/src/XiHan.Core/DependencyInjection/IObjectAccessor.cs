#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IObjectAccessor
// Guid:a9a7abde-1115-4199-8b71-936733c2fa26
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 04:08:09
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.DependencyInjection;

/// <summary>
/// 对象访问器接口
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IObjectAccessor<out T>
{
    /// <summary>
    /// 泛型对象
    /// </summary>
    T? Value { get; }
}