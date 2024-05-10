#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ObjectAccessor
// Guid:e136c6c9-28bb-4608-8a73-8621222ab0c8
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 04:11:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.DependencyInjection;

/// <summary>
/// 对象访问器接口
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectAccessor<T> : IObjectAccessor<T>
{
    /// <summary>
    /// 泛型对象
    /// </summary>
    public T? Value { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public ObjectAccessor()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="obj"></param>
    public ObjectAccessor(T? obj)
    {
        Value = obj;
    }
}