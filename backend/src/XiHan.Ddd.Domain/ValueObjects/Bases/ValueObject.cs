#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ValueObject
// Guid:f73c2797-2925-4c9b-9c3d-afe7cbc66565
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/27 0:03:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;

namespace XiHan.Ddd.Domain.ValueObjects.Bases;

/// <summary>
/// 值对象基类
/// 值对象是不可变的对象，用于封装小规模数据，通常由其属性值来定义其相等性
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// 获取值对象的属性值集合
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// 确定指定的对象是否等于当前对象
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }
        return left is null || left.Equals(right);
    }

    /// <summary>
    /// 确定指定的对象是否不等于当前对象
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !(left == right);
    }

    /// <summary>
    /// 确定指定的对象是否等于当前对象
    /// </summary>
    /// <param name="obj">要与当前对象进行比较的对象</param>
    /// <returns>如果指定的对象等于当前对象，则为 true，否则为 false</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        ValueObject other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    /// 用作特定类型的哈希函数
    /// </summary>
    /// <returns>当前对象的哈希代码</returns>
    public override int GetHashCode()
    {
        return GetEqualityComponents().Select(x => x != null ? x.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    /// 返回表示当前对象的字符串
    /// </summary>
    /// <returns>表示当前对象的字符串</returns>
    public override string ToString()
    {
        var items = GetEqualityComponents().ToList();

        StringBuilder sb = new();
        sb.Append('[');
        foreach (var item in items)
        {
            sb.Append(item.ToString());
            sb.Append(',');
        }
        // 移除最后的","
        if (items.Count > 0)
        {
            sb.Length -= 1;
        }
        sb.Append(']');

        return sb.ToString();
    }
}