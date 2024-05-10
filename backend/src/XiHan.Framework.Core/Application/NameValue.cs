#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:NameValue
// Guid:98b7df42-f677-400c-a4aa-08e2b4e636be
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/27 0:34:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.Application;

/// <summary>
/// 可用于存储名称/值（或键/值）对
/// </summary>
[Serializable]
public class NameValue : NameValue<string>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public NameValue()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public NameValue(string name, string value)
    {
        Name = name;
        Value = value;
    }
}

/// <summary>
/// 可用于存储名称/值（或键/值）对
/// </summary>
[Serializable]
public class NameValue<T>
{
    /// <summary>
    /// 键
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// 值
    /// </summary>
    public T Value { get; set; } = default!;

    /// <summary>
    /// 构造函数
    /// </summary>
    public NameValue()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public NameValue(string name, T value)
    {
        Name = name;
        Value = value;
    }
}